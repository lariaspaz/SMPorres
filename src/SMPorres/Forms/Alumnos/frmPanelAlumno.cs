using SMPorres.Lib.AppForms;
using SMPorres.Lib.Validations;
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
    public partial class frmPanelAlumno : FormBase
    {
        private FormValidations _validator;

        public frmPanelAlumno()
        {
            InitializeComponent();
            _validator = new FormValidations(this, errorProvider1);
        }

        private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {

        }

        private void txtNroDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConsultarDatos();
            }
        }

        private void ConsultarDatos()
        {
            var a = AlumnosRepository.BuscarAlumnoPorNroDocumento(txtNroDocumento.DecValue);
            if (_validator.Validar(txtNroDocumento, a != null, "No existe el alumno"))
            {
                return;
            }
            txtNombre.Text = a.Apellido + ", " + a.Nombre;
        }
    }
}
