using SMPorres.Lib.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMPorres.Models;
using SMPorres.Repositories;
using SMPorres.Forms.Domicilios;

namespace SMPorres.Forms.Alumnos
{
    public partial class frmEdición : Lib.AppForms.FormBase
    {
        private FormValidations _validator;

        int idDom = 0;

        public frmEdición()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva transacción";
            txtNombre.Select();
            _validator = new FormValidations(this, errorProvider1);
            
            datosIniciales();
        }

        public void datosIniciales()
        {
            cargarProvincias();
            cargarIdTipoDoc();
            cbDomicilio.SelectedValue = 0;
            cbTipoDoc.SelectedValue = 0;
        }

        public frmEdición(Alumno alumno) : this()
        {
            this.Text = "Edición de transacción";
            txtNombre.Text = alumno.Nombre;
            txtApellido.Text = alumno.Apellido;
            cbTipoDoc.SelectedIndex = alumno.IdTipoDocumento - 1;
            txtNumDocumento.Text = alumno.NroDocumento.ToString();
            dtpFechaNac.Text = alumno.FechaNacimiento.ToString();
            txtEmail.Text = alumno.EMail;
            txtDireccion.Text = alumno.Direccion;
            cargarDomicilios(alumno.IdDomicilio);
            ckEstado.Checked = alumno.Estado == 1;
        }

        private void cargarDomicilios(int idDomicilio)
        {
            Models.Domicilio d = AlumnosRepository.ObtenerDomicilio(idDomicilio);

            cargarProvincias();
            cbProvincia.SelectedValue = d.IdProvincia;

            cargarDepartamento(d.IdProvincia);
            cbDepartamento.SelectedValue = d.IdDepartamento;

            cargarLocalidad(d.IdDepartamento);
            cbLocalidad.SelectedValue = d.IdLocalidad;

            cargarBarrio(d.IdLocalidad);
            cbBarrio.SelectedValue = d.IdBarrio;            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            
            /*Busca domicilio asocido*/
            idDom = DomiciliosRepository.obtenerIdDomicilio(Convert.ToInt32(cbProvincia.SelectedValue), Convert.ToInt32(cbDepartamento.SelectedValue),
                    Convert.ToInt32(cbLocalidad.SelectedValue), Convert.ToInt32(cbBarrio.SelectedValue));

            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void cargarProvincias()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var Provincias =
                    (
                        from p in db.Provincias
                        select new
                        {
                            Id = p.Id,
                            Provincia = p.Nombre
                        }
                    ).ToList();

                cbProvincia.DataSource = Provincias;
                cbProvincia.DisplayMember = "Provincia";
                cbProvincia.ValueMember = "Id";
            }
        }
        private void cargarDepartamento(Int64 idProvincia)
        {
            using (var db = new Models.SMPorresEntities())
            {
                var Departamentos =
                    (
                        from d in db.Departamentos
                        where d.IdProvincia == idProvincia //c.idCarrera == idCarrera
                        select new
                        {
                            Id = d.Id,
                            Departamento = d.Nombre
                        }
                    ).ToList();

                cbDepartamento.DataSource = Departamentos;
                cbDepartamento.DisplayMember = "Departamento";
                cbDepartamento.ValueMember = "Id";
            }
        }
        private void cargarLocalidad(Int64 idDepartamento)
        {
            using (var db = new Models.SMPorresEntities())
            {
                var Localidades =
                    (
                        from l in db.Localidades
                        where l.IdDepartamento == idDepartamento
                        select new
                        {
                            Id = l.Id,
                            Localidad = l.Nombre
                        }
                    ).ToList();

                cbLocalidad.DataSource = Localidades;
                cbLocalidad.DisplayMember = "Localidad";
                cbLocalidad.ValueMember = "Id";
            }
        }
        private void cargarBarrio(Int64 idLocalidad)
        {
            using (var db = new Models.SMPorresEntities())
            {
                var Barrios =
                    (
                        from b in db.Barrios
                        where b.IdLocalidad == idLocalidad
                        select new
                        {
                            Id = b.Id,
                            Barrio = b.Nombre
                        }
                    ).ToList();

                cbBarrio.DataSource = Barrios;
                cbBarrio.DisplayMember = "Barrio";
                cbBarrio.ValueMember = "Id";
            }
        }

        private void cargarIdTipoDoc()
        {
            using (var db = new Models.SMPorresEntities())
            {
                var TiposDoc =
                    (
                        from d in db.TiposDocumento
                        select new
                        {
                            Id = d.Id,
                            TipoDoc = d.Descripcion
                        }
                    ).ToList();

                cbTipoDoc.DataSource = TiposDoc;
                cbTipoDoc.DisplayMember = "TipoDoc";
                cbTipoDoc.ValueMember = "Id";
            }
        }
       
        private bool ValidarDatos()
        {
            return
                _validator.Validar(txtNombre, Nombre != "", "Debe contener texto") &&
                _validator.Validar(txtApellido, Apellido != "", "Debe contener texto") &&
                _validator.Validar(txtNumDocumento, NroDoc > 0, "No puede ser menor o igual que cero") &&
                _validator.Validar(txtDireccion, Dirección != "", "Debe contener texto");
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text;
            }
        }

        public string Apellido
        {
            get
            {
                return txtApellido.Text;
            }
        }

        public decimal NroDoc
        {
            get
            {
                return txtNumDocumento.DecValue;
            }
        }

        public byte Estado
        {
            get
            {
                return (byte)(ckEstado.Checked ? 1 : 0);
            }
        }

        public DateTime  FechaNacimiento
        {
            get
            {
                return dtpFechaNac.Value;
            }
        }

        public string Email
        {
            get
            {
                return txtEmail.Text;
            }
        }

        public string Dirección
        {
            get
            {
                return txtDireccion.Text;
            }
        }

        public int IdTipoDoc
        {
            get
            {
                return Convert.ToInt32(cbTipoDoc.SelectedValue);
            }
        }

        public Int32 IdDomicilio
        {
            get
            {
                return idDom;
            }
        }

        private void cbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbDepartamento.DisplayMember = "";
            int d = Convert.ToInt32(cbProvincia.SelectedValue);
            cargarDepartamento(d);
        }

        private void cbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbLocalidad.DisplayMember = "";
            int l = Convert.ToInt32(cbDepartamento.SelectedValue);
            cargarLocalidad(l);
        }

        private void cbLocalidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbBarrio.DisplayMember = "";
            int b = Convert.ToInt32(cbLocalidad.SelectedValue);
            cargarBarrio(b);
        }

        private void btnDepartamentos_Click(object sender, EventArgs e)
        {
            using (var f = new frmDomicilios((Int32)cbProvincia.SelectedValue, cbProvincia.Text))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var d = DomiciliosRepository.InsertarDepartamento((Int32)cbProvincia.SelectedValue, f.Hijo);

                        cbDepartamento.DisplayMember = "";
                        cargarDepartamento((Int32)cbProvincia.SelectedValue);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void btnLocalidad_Click(object sender, EventArgs e)
        {
            using (var f = new frmDomicilios((Int32)cbProvincia.SelectedValue, (Int32)cbDepartamento.SelectedValue, cbDepartamento.Text))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var loc = DomiciliosRepository.InsertarLocalidad((Int32)cbDepartamento.SelectedValue, f.Hijo);
                        cbLocalidad.DisplayMember = "";
                        int l = Convert.ToInt32(cbDepartamento.SelectedValue);
                        cargarLocalidad(l);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }

        private void btnBarrio_Click(object sender, EventArgs e)
        {
            using (var f = new frmDomicilios((Int32)cbProvincia.SelectedValue, (Int32)cbDepartamento.SelectedValue, 
                (Int32)cbLocalidad.SelectedValue, cbLocalidad.Text))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var loc = DomiciliosRepository.InsertarBarrio((Int32)cbLocalidad.SelectedValue, f.Hijo);
                        cbBarrio.DisplayMember = "";
                        int b = Convert.ToInt32(cbLocalidad.SelectedValue);
                        cargarBarrio(b);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error al intentar grabar los datos: \n" + ex.Message);
                    }
                }
            }
        }
    }
}
