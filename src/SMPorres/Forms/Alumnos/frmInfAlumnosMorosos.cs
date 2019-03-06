using SMPorres.Lib.AppForms;
using SMPorres.Models;
using SMPorres.Reports.DataSet;
using SMPorres.Reports.Designs;
using SMPorres.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Alumnos
{
    public partial class frmInfAlumnosMorosos : FormBase
    {
        private enum TiposInforme
        {
            AlumnosAlDía = 1,
            AlumnosMorosos = 2
        }

        private enum TiposBecados
        {
            Todos = 0,
            SinBeca = 1,
            ConBeca = 2
        }

        public frmInfAlumnosMorosos()
        {
            InitializeComponent();

            var qry = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            qry.Insert(0, new Carrera { Id = 0, Nombre = "(Todas las carreras)" });
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";
            cbCarreras.DataSource = qry;

            CargarTiposInforme();
            CargarTiposBecados();
        }

        private void CargarTiposBecados()
        {
            var beca = new Dictionary<TiposBecados, string>();
            beca.Add(TiposBecados.Todos, "(Todos los alumnos)");
            beca.Add(TiposBecados.SinBeca, "Alumnos sin beca asignada");
            beca.Add(TiposBecados.ConBeca, "Alumnos con beca asignada");
            cbBeca.DataSource = new BindingSource(beca, null);
            cbBeca.ValueMember = "Key";
            cbBeca.DisplayMember = "Value";
            cbBeca.SelectedIndex = 0;
        }

        private void CargarTiposInforme()
        {
            var tipos = new Dictionary<TiposInforme, string>();
            tipos.Add(TiposInforme.AlumnosAlDía, "Alumnos al día");
            tipos.Add(TiposInforme.AlumnosMorosos, "Alumnos morosos");
            cbTipo.DataSource = new BindingSource(tipos, null);
            cbTipo.ValueMember = "Key";
            cbTipo.DisplayMember = "Value";
            cbTipo.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            using (var dt = ObtenerDatos())
            {
                if (dt.Rows.Count > 0)
                {
                    MostrarReporte(dt);
                }
                else
                {
                    ShowError("No hay ningún registro que coincida con su consulta.");
                }
            }
        }

        private void MostrarReporte(DataTable dt)
        {
            using (var reporte = new AlumnosMorosos())
            {
                string título = cbTipo.Text;
                string fecha = "Al " + dtFecha.Value.Date.ToString("dd/MM/yyyy");
                string curso = cbCursos.Text;
                if (IdCurso == 0) curso = curso.Replace("(", "").Replace(")", "");
                string carrera = cbCarreras.Text;
                if (IdCarrera == 0) carrera = carrera.Replace("(", "").Replace(")", "");
                var subTítulo = fecha + " - " + curso + " - " + carrera;
                reporte.Database.Tables["AlumnoMoroso"].SetDataSource(dt);
                using (var f = new frmReporte(reporte, título, subTítulo)) f.ShowDialog();
            }
        }

        private int IdCarrera
        {
            get
            {
                return Convert.ToInt32(cbCarreras.SelectedValue);
            }
        }

        private int IdCurso
        {
            get
            {
                return Convert.ToInt32(cbCursos.SelectedValue);
            }
        }

        private TiposInforme TipoInforme
        {
            get
            {
                return (TiposInforme)cbTipo.SelectedValue;
            }
        }

        private TiposBecados TipoBecado
        {
            get
            {
                return (TiposBecados)cbBeca.SelectedValue;
            }
        }

        private DateTime Fecha
        {
            get
            {
                return dtFecha.Value.Date;
            }
        }

        private void cbCarreras_SelectedValueChanged(object sender, EventArgs e)
        {
            var items = CursosRepository.ObtenerCursosPorIdCarrera(IdCarrera).OrderBy(c => c.Nombre).ToList();
            items.Insert(0, new Curso { Id = 0, Nombre = "(Todos los cursos)" });
            cbCursos.DataSource = items;
            cbCursos.DisplayMember = "Nombre";
            cbCursos.ValueMember = "Id";
            cbCursos.SelectedIndex = 0;
        }

        private DataTable ObtenerDatos()
        {
            var tabla = new dsImpresiones.AlumnoMorosoDataTable();
            var alumnos = StoredProcs.ConsAlumnosMorosos(Fecha, (short)TipoInforme, IdCarrera, IdCurso, (short) TipoBecado);
            foreach (var a in alumnos)
            {
                string nrodoc = a.NroDocumento.ToString();
                string vtoCuota = a.VtoCuota.HasValue ? a.VtoCuota.Value.ToString("dd/MM/yy") : "";
                string fechaPago = a.FechaPago.HasValue ? a.FechaPago.Value.ToString("dd/MM/yy") : "";
                string importeCuota = a.ImporteCuota.ToString("###,##0.00");
                string importePagado = a.ImportePagado.HasValue ? a.ImportePagado.Value.ToString("###,##0.00") : "";
                string beca = a.Beca.HasValue ? a.Beca.Value.ToString("0\\%") : "";
                tabla.AddAlumnoMorosoRow(a.IdCurso, a.Curso, a.Carrera, a.TipoDocumento, nrodoc, a.Nombre,
                    a.Apellido, vtoCuota, fechaPago, a.Cuota ?? 0, importeCuota, importePagado, beca);
            }
            return tabla;
        }
    }
}
