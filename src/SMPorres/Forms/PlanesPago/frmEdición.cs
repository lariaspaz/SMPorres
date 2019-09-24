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

namespace SMPorres.Forms.PlanesPago
{
    public partial class frmEdición : FormBase
    {
        public frmEdición(string alumno, string cursoView, string nombreCurso)
        {
            InitializeComponent();
            this.Text = "Nuevo plan de pago";
            txtAlumno.Text = alumno;
            txtCurso.Text = cursoView;
            var Curso = CursosRepository.ObtenerCursoPorNombre(nombreCurso);

            cargarCbModalidad(Convert.ToInt16( Curso.Modalidad) );
            CompletaTxtCuotas(CursosRepository.ObtieneMinCuota(Curso.Modalidad),
                        CursosRepository.ObtieneMaxCuota(Curso.Modalidad));

            txtPorcentajeBeca.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        private void cargarCbModalidad(int modalidad)
        {
            var modalidades = new Dictionary<int, string>();
            modalidades.Add(1, "Anual");
            modalidades.Add(2, "Primer cuatrimestre");
            modalidades.Add(3, "Segundo cuatrimestre");
            modalidades.Add(4, "Sin cursado");
            cbModalidad.DataSource = new BindingSource(modalidades, null);
            cbModalidad.ValueMember = "Key";
            cbModalidad.DisplayMember = "Value";
            cbModalidad.SelectedIndex = Convert.ToInt16(modalidad) - 1;
        }

        private void CompletaTxtCuotas(short minC, short maxC)
        {
            string txt = "";
            txt = minC.ToString() + " - " + maxC.ToString();
            txtCuota.Text = String.Format(txt, ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo);
        }

        public frmEdición(string alumno, string cursoView, string nombreCurso, PlanPago plan) : this(alumno, cursoView, nombreCurso)
        {
            this.Text = "Edición de plan de pago";
            txtCuota.Text = String.Format("{0} de {1}", plan.NroCuota, plan.CantidadCuotas);
            cargarCbModalidad(Convert.ToInt16(plan.Modalidad));
            txtPorcentajeBeca.DecValue = plan.PorcentajeBeca;
        }

        public short PorcentajeBeca
        {
            get
            {
                return (short) txtPorcentajeBeca.IntValue;
            }
        }

        public int Modalidad
        {
            private set
            {
                cbModalidad.SelectedValue = value;
            }
            get
            {
                return (int)cbModalidad.SelectedIndex + 1;
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
            return _validator.Validar(txtPorcentajeBeca, txtPorcentajeBeca.DecValue >= 0, "Debe ser mayor que 0");
        }

        private void cbModalidad_SelectedValueChanged(object sender, EventArgs e)
        {
            CompletaTxtCuotas(CursosRepository.ObtieneMinCuota(Modalidad),CursosRepository.ObtieneMaxCuota(Modalidad));
        }
    }
}
