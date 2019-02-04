using SMPorres.Lib.AppForms;
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
    public partial class frmInfAlumnosPorEstadoCondiciones : FormBase
    {
        public frmInfAlumnosPorEstadoCondiciones()
        {
            InitializeComponent();

            cbCarreras.DataSource = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";

            cbCursos.Enabled = false;
            chkVerCursos.Checked = false;
            cbEstado.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (chkVerCursos.Checked)
            {
                using (var f = new frmInfAlumnosPorEstado(IdCarrera, IdCurso, Estado)) f.ShowDialog();
            }
            else
            {
                using (var f = new frmInfAlumnosPorEstado(IdCarrera, Estado)) f.ShowDialog();
            }
        }

        private void chkVerCursos_CheckedChanged(object sender, EventArgs e)
        {
            cbCursos.Enabled = chkVerCursos.Checked;
            cbCursos.Enabled = chkVerCursos.Checked;
        }

        private int IdCarrera
        {
            get
            {
                return ((Models.Carrera)cbCarreras.SelectedItem).Id;
            }
        }

        private int IdCurso
        {
            get
            {
                return ((Models.Curso)cbCursos.SelectedItem).Id;
            }
        }

        private int Estado
        {
            get
            {
                if (cbEstado.SelectedItem.ToString() == "Activo") return 1;

                return 2;
            }
        }

        private void cbCarreras_SelectedValueChanged(object sender, EventArgs e)
        {
            var items = CursosRepository.ObtenerCursosPorIdCarrera(IdCarrera).OrderBy(c => c.Nombre).ToList();

            cbCursos.DataSource = items;
            cbCursos.DisplayMember = "Nombre";
            cbCursos.ValueMember = "Id";
            cbCursos.SelectedItem = items.FirstOrDefault();
            cbCursos.Text = "Todos los cursos";
        }
    }
}
