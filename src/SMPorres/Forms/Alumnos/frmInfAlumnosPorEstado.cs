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
    public partial class frmInfAlumnosPorEstado : FormBase
    {
        public frmInfAlumnosPorEstado()
        {
            InitializeComponent();

            var qry = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            qry.Insert(0, new Carrera { Id = 0, Nombre = "(Todas las carreras)" });
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";
            cbCarreras.DataSource = qry;

            var estados = new Dictionary<EstadoAlumno, string>();
            estados.Add(EstadoAlumno.Activo, "Activo");
            estados.Add(EstadoAlumno.Baja, "Baja");
            cbEstado.DataSource = new BindingSource(estados, null);
            cbEstado.ValueMember = "Key";
            cbEstado.DisplayMember = "Value";
            cbEstado.SelectedIndex = 0;
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
            using (var reporte = new AlumnosPorEstado())
            {
                string título = "Alumnos por Estado";
                string curso = cbCursos.Text;
                if (IdCurso == 0) curso = curso.Replace("(", "").Replace(")", "");
                string carrera = cbCarreras.Text;
                if (IdCarrera == 0) carrera = carrera.Replace("(", "").Replace(")", "");
                var subTítulo = curso + " - " + carrera;
                reporte.Database.Tables["EstadoAlumno"].SetDataSource(dt);
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

        private EstadoAlumno Estado
        {
            get
            {
                return (EstadoAlumno)cbEstado.SelectedValue;
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
            var tabla = new dsImpresiones.EstadoAlumnoDataTable();
            var alumnos = AlumnosRepository.ObtenerAlumnosPorEstado(IdCarrera, IdCurso, Estado);
            foreach (var a in alumnos)
            {
                tabla.AddEstadoAlumnoRow(a.IdCarrera, a.Carrera, a.IdCurso, a.Curso, a.Nombre, a.Apellido, a.EMail, a.LeyendaEstado(), a.Documento);
            }
            return tabla;
        }
    }
}
