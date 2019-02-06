using SMPorres.Lib.AppForms;
using SMPorres.Models;
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
        static string _título = "Informe de Alumnos por Estado";
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
                    ShowError("No hay datos para mostrar.");
                }
            }
        }

        private static void MostrarReporte(DataTable dt)
        {
            using (var reporte = new AlumnosPorEstado())
            {
                reporte.Database.Tables["dtAlumnosPorEstado"].SetDataSource(dt);
                using (var f = new frmReporte(_título, reporte)) f.ShowDialog();
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
            var dt = CrearDataTable();
            var alumnos = AlumnosRepository.ObtenerAlumnosPorEstado(IdCarrera, IdCurso, Estado);
            string títuloInforme = _título + " de " + cbCursos.Text + " de " + cbCarreras.Text;
            foreach (var a in alumnos)
            {
                dt.Rows.Add(String.Format("{0} de {1}", a.Curso, a.Carrera), a.Nombre, a.Apellido, a.EMail, a.LeyendaEstado(), títuloInforme, a.Documento);
            }
            return dt;
        }

        private DataTable CrearDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CarreraCurso", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Apellido", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Estado", typeof(string));
            dt.Columns.Add("Título", typeof(string));
            dt.Columns.Add("Documento", typeof(string));
            return dt;
        }
    }
}
