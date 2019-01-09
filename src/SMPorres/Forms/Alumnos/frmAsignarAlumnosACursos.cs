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
    public partial class frmAsignarAlumnosACursos : FormBase
    {
        public frmAsignarAlumnosACursos()
        {
            InitializeComponent();
            cbCarreras.DataSource = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";
        }

        private int IdCarrera
        {
            get
            {
                return ((Models.Carrera)cbCarreras.SelectedItem).Id;
            }
        }

        private void cbCarreras_SelectedValueChanged(object sender, EventArgs e)
        {
            cbCursos.DataSource = CursosRepository.ObtenerCursosPorIdCarrera(IdCarrera).OrderBy(c => c.Nombre).ToList();
            cbCursos.DisplayMember = "Nombre";
            cbCursos.ValueMember = "Id";
        }

        private void cbCursos_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}
