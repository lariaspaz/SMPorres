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

        public frmEdición()
        {
            InitializeComponent();
            this.Text = "Nuevo alumno";
            txtNombre.Select();
            _validator = new FormValidations(this, errorProvider1);
            CargarProvincias();
            CargarDepartamentos(IdProvincia);
            CargarLocalidades(IdDepartamento);
            CargarBarrios(IdLocalidad);
            CargarTiposDocumento();
        }

        public frmEdición(Alumno alumno) : this()
        {
            this.Text = "Edición de alumno";
            txtNombre.Text = alumno.Nombre;
            txtApellido.Text = alumno.Apellido;
            cbTipoDoc.SelectedIndex = alumno.IdTipoDocumento - 1;
            txtNumDocumento.Text = alumno.NroDocumento.ToString();
            dtpFechaNac.Text = alumno.FechaNacimiento.ToString();
            txtEmail.Text = alumno.EMail;
            txtDireccion.Text = alumno.Direccion;
            CargarDomicilio(alumno.IdDomicilio);
            ckEstado.Checked = alumno.Estado == 1;
        }

        private void CargarDomicilio(int idDomicilio)
        {
            Models.Domicilio d = AlumnosRepository.ObtenerDomicilio(idDomicilio);

            CargarProvincias();
            cbProvincia.SelectedValue = d.IdProvincia;

            CargarDepartamentos(d.IdProvincia);
            cbDepartamento.SelectedValue = d.IdDepartamento;

            CargarLocalidades(d.IdDepartamento);
            cbLocalidad.SelectedValue = d.IdLocalidad;

            CargarBarrios(d.IdLocalidad);
            cbBarrio.SelectedValue = d.IdBarrio;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            /*Busca domicilio asocido*/
            //_idDomicilio = DomiciliosRepository.obtenerIdDomicilio(Convert.ToInt32(cbProvincia.SelectedValue), Convert.ToInt32(cbDepartamento.SelectedValue),
            //        Convert.ToInt32(cbLocalidad.SelectedValue), Convert.ToInt32(cbBarrio.SelectedValue));

            if (this.ValidarDatos())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void CargarProvincias()
        {
            cbProvincia.DataSource = null;
            using (var db = new Models.SMPorresEntities())
            {
                var provincias = (from p in db.Provincias select new { Id = p.Id, Provincia = p.Nombre })
                                    .ToList();
                cbProvincia.DataSource = provincias;
                cbProvincia.DisplayMember = "Provincia";
                cbProvincia.ValueMember = "Id";
            }
        }

        private void CargarDepartamentos(int idProvincia)
        {
            cbDepartamento.DataSource = null;
            using (var db = new Models.SMPorresEntities())
            {
                var deptos = (from d in db.Departamentos
                              where d.IdProvincia == idProvincia
                              select new
                              {
                                  Id = d.Id,
                                  Departamento = d.Nombre
                              }
                             ).ToList();

                cbDepartamento.DataSource = deptos;
                cbDepartamento.DisplayMember = "Departamento";
                cbDepartamento.ValueMember = "Id";
            }
        }

        private void CargarLocalidades(int idDepartamento)
        {
            cbLocalidad.DataSource = null;
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

        private void CargarBarrios(int idLocalidad)
        {
            cbBarrio.DataSource = null;
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

        private void CargarTiposDocumento()
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
                cbTipoDoc.SelectedIndex = 0;
            }
        }

        private bool ValidarDatos()
        {
            return
                _validator.Validar(txtNombre, !String.IsNullOrEmpty(Nombre), "No puede estar vacío") &&
                _validator.Validar(txtApellido, !String.IsNullOrEmpty(Apellido), "No puede estar vacío") &&
                _validator.Validar(txtNumDocumento, NroDocumento > 0, "No puede ser menor o igual que cero") &&
                _validator.Validar(txtDireccion, Dirección != "", "No puede estar vacío");
        }

        public string Nombre
        {
            get
            {
                return txtNombre.Text.Trim();
            }
        }

        public string Apellido
        {
            get
            {
                return txtApellido.Text.Trim();
            }
        }

        public decimal NroDocumento
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

        public DateTime FechaNacimiento
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
                return txtEmail.Text.Trim();
            }
        }

        public string Dirección
        {
            get
            {
                return txtDireccion.Text.Trim();
            }
        }

        public int IdTipoDocumento
        {
            get
            {
                return Convert.ToInt32(cbTipoDoc.SelectedValue);
            }
        }

        public int IdDomicilio
        {
            get
            {
                return DomiciliosRepository.obtenerIdDomicilio(
                    Convert.ToInt32(cbProvincia.SelectedValue),
                    Convert.ToInt32(cbDepartamento.SelectedValue),
                    Convert.ToInt32(cbLocalidad.SelectedValue),
                    Convert.ToInt32(cbBarrio.SelectedValue)
                    );
            }
        }

        public int IdProvincia
        {
            get
            {
                return Convert.ToInt32(cbProvincia.SelectedValue);
            }
        }

        public int IdDepartamento
        {
            get
            {
                return Convert.ToInt32(cbDepartamento.SelectedValue);
            }
        }

        public int IdLocalidad
        {
            get
            {
                return Convert.ToInt32(cbLocalidad.SelectedValue);
            }
        }

        private void cbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarDepartamentos(IdProvincia);
        }

        private void cbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarLocalidades(IdDepartamento);
        }

        private void cbLocalidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargarBarrios(IdLocalidad);
        }

        private void btnDepartamentos_Click(object sender, EventArgs e)
        {
            using (var f = new frmDomicilios(IdProvincia, cbProvincia.Text))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DomiciliosRepository.InsertarDepartamento(IdProvincia, f.Hijo);
                        CargarDepartamentos(IdProvincia);
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
            using (var f = new frmDomicilios(IdProvincia, IdDepartamento, cbDepartamento.Text))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DomiciliosRepository.InsertarLocalidad(IdDepartamento, f.Hijo);
                        CargarLocalidades(IdDepartamento);
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
            using (var f = new frmDomicilios(IdProvincia, IdDepartamento, IdLocalidad, cbLocalidad.Text))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var loc = DomiciliosRepository.InsertarBarrio(IdLocalidad, f.Hijo);
                        CargarBarrios(IdLocalidad);
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
