using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMPorres.Repositories;
using SMPorres.Forms.Alumnos;
using SMPorres.Lib.Validations;

namespace SMPorres.Forms.Domicilios
{
    public partial class frmDomicilios : Lib.AppForms.FormBase
    {
        int _id = 0;

        public frmDomicilios()
        {
            InitializeComponent();
        }

        //Carga Departamento
        public frmDomicilios(Int32 idProvincia, string provincia)
        {
            InitializeComponent();

            this.Text = "Nueva transacción";
            lblPadre.Text = "Provincia";
            if (provincia != null) txtPadre.Text = provincia.ToString();
            
            lblHijo.Text = "Ingrese el Departamento que desea agregar";
            _id = idProvincia;
        }

        //carga Localidad
        public frmDomicilios(Int32 idProvincia, Int32 idDepartamento, string departamento)
        {
            InitializeComponent();

            this.Text = "Nueva transacción";
            lblPadre.Text = "Departamento";
            if (departamento != null) txtPadre.Text = departamento.ToString();

            lblHijo.Text = "Ingrese la Localidad que desea agregar";
            _id = idDepartamento;
        }

        //carga Barrio
        public frmDomicilios(Int32 idProvincia, Int32 idDepartamento, Int32 idLocalidad, string barrio)
        {
            InitializeComponent();

            this.Text = "Nueva transacción";
            lblPadre.Text = "Localidad";
            if (barrio != null) txtPadre.Text = barrio.ToString();

            lblHijo.Text = "Ingrese el barrio que desea agregar";
            _id = idLocalidad;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            if (txtHijo.Text != "")
            {
                DialogResult = DialogResult.OK;
            }
        }

        public string Hijo
        {
            get
            {
                return txtHijo.Text;
            }
        }

        private void frmDomicilios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) btnCancelar.PerformClick();
        }
    }
}
