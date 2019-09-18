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
            //txtCuota.Text = String.Format("1 de 10", ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo);
            string txt = CursosRepository.ObtieneMinCuota(Curso.Modalidad).ToString() + " - " +
                        CursosRepository.ObtieneMaxCuota(Curso.Modalidad).ToString();
            txtCuota.Text = String.Format(txt, ConfiguracionRepository.ObtenerConfiguracion().CicloLectivo);
            txtModalidad.Text = CursosRepository.ObtenerModalidadCurso(Curso.Id);

            txtPorcentajeBeca.Select();
            _validator = new FormValidations(this, errorProvider1);
        }

        public frmEdición(string alumno, string cursoView, string nombreCurso, PlanPago plan) : this(alumno, cursoView, nombreCurso)
        {
            this.Text = "Edición de plan de pago";
            txtCuota.Text = String.Format("{0} de {1}", plan.NroCuota, plan.CantidadCuotas);
            txtPorcentajeBeca.DecValue = plan.PorcentajeBeca;
        }

        public short PorcentajeBeca
        {
            get
            {
                return (short) txtPorcentajeBeca.IntValue;
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
    }
}
