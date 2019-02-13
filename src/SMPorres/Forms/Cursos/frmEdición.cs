using SMPorres.Lib.AppForms;
using SMPorres.Lib.Validations;
using SMPorres.Models;
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

namespace SMPorres.Forms.Cursos
{
    public partial class frmEdición : FormBase
    {
        public frmEdición()
        {
            InitializeComponent();
            cbCarreras.DataSource = CarrerasRepository.ObtenerCarreras().OrderBy(c => c.Nombre).ToList();
            cbCarreras.DisplayMember = "Nombre";
            cbCarreras.ValueMember = "Id";
            this.Text = "Nuevo curso";
            txtNombre.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(Curso curso) : this()
        {
            this.Text = "Edición de curso";
            txtNombre.Text = curso.Nombre;
            cbCarreras.SelectedValue = curso.IdCarrera;
            txtImporteMatrícula.DecValue = curso.ImporteMatricula;
            txtImporteCuota.DecValue = curso.ImporteCuota;
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text;
            }
        }

        public int IdCarrera
        {
            get
            {
                return ((Carrera)cbCarreras.SelectedItem).Id;
            }
        }

        public decimal ImporteMatrícula
        {
            get
            {
                return txtImporteMatrícula.DecValue;
            }
        }

        public decimal ImporteCuota
        {
            get
            {
                return txtImporteCuota.DecValue;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            return _validator.Validar(txtNombre, !String.IsNullOrEmpty(txtNombre.Text.Trim()), "No puede estar vacío") &&
                _validator.Validar(txtImporteCuota, txtImporteCuota.DecValue >= 0, "No puede ser menor que 0");
        }        
    }
}
