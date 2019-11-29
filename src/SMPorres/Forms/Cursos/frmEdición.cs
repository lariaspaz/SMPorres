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
            var modalidades = new Dictionary<int, string>();
            modalidades.Add(1, "Anual");
            modalidades.Add(2, "Primer cuatrimestre");
            modalidades.Add(3, "Segundo cuatrimestre");
            modalidades.Add(4, "Sin cursado");
            cbModalidad.DataSource = new BindingSource(modalidades, null);
            cbModalidad.ValueMember = "Key";
            cbModalidad.DisplayMember = "Value";
            cbModalidad.SelectedIndex = 0;
            this.Text = "Nuevo curso";
            ckEstado.Checked = true;
            txtNombre.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(Curso curso) : this()
        {
            this.Text = "Edición de curso";
            txtNombre.Text = curso.Nombre;
            cbCarreras.SelectedValue = curso.IdCarrera;
            txtImporteMatrícula.DecValue = curso.ImporteMatricula;
            txtDescuentoPagoAdelantado.DecValue = Convert.ToDecimal(curso.DescuentoMatricula);
            dtPagoAdelantadoHasta.Value = Convert.ToDateTime(curso.FechaVencDescuento);
            txtCuota1.DecValue = Convert.ToDecimal(curso.Cuota1);
            txtCuota2.DecValue = Convert.ToDecimal(curso.Cuota2);
            txtCuota3.DecValue = Convert.ToDecimal(curso.Cuota3);
            txtImporteCuota.DecValue = curso.ImporteCuota;
            cbModalidad.SelectedIndex = (int)curso.Modalidad - 1;
        }

        public int Modalidad
        {
            private set
            {
                cbModalidad.SelectedValue = value;
            }
            get
            {
                return (int)cbModalidad.SelectedValue;
            }
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

        public decimal DescuentoPagoAdelantadoMatricula
        {
            get
            {
                return txtDescuentoPagoAdelantado.DecValue;
            }
        }

        public DateTime PagoAdelantadoHasta
        {
            get
            {
                return dtPagoAdelantadoHasta.Value.Date;
            }
        }

        public decimal ImporteCuota1
        {
            get
            {
                return txtCuota1.DecValue;
            }
        }

        public decimal ImporteCuota2
        {
            get
            {
                return txtCuota2.DecValue;
            }
        }

        public decimal ImporteCuota3
        {
            get
            {
                return txtCuota3.DecValue;
            }

        }

        public Int16 Estado
        {
            get
            {
                return (Int16)(ckEstado.Checked ? (short)EstadoCurso.Activo : (short)EstadoCurso.Baja);
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
            decimal matrícula = txtImporteMatrícula.DecValue - txtCuota1.DecValue - txtCuota2.DecValue - txtCuota3.DecValue;
            decimal descuentoMatrícula = txtImporteMatrícula.DecValue - txtDescuentoPagoAdelantado.DecValue;
            return _validator.Validar(txtNombre, !String.IsNullOrEmpty(txtNombre.Text.Trim()), "No puede estar vacío") &&
                _validator.Validar(txtImporteCuota, txtImporteCuota.DecValue >= 0, "No puede ser menor que 0") &&
                _validator.Validar(txtImporteMatrícula, matrícula == 0, "El importe de matrícula es igual a la suma de sus cuotas") &&
                _validator.Validar(txtImporteMatrícula, descuentoMatrícula > 0, "El importe de matrícula debe ser mayor al descuento");
        }        
    }
}

