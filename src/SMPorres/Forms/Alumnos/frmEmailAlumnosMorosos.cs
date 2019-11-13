using SMPorres.Lib.AppForms;
using SMPorres.Models;
using SMPorres.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Forms.Alumnos
{
    public partial class frmEmailAlumnosMorosos : FormBase
    {
        public frmEmailAlumnosMorosos()
        {
            InitializeComponent();
            ConsultarDatos();
        }

        private void ConsultarDatos()
        {
            IList<Carrera> carreras = CarrerasRepository.ObtenerCarreras();
            foreach (var item in carreras)
            {
                var morosos = AlumnosMorososRepository.ObtenerAlumnosMorosos(item.Id);
                //dgvDatos.SetDataSource(morosos);

                //MessageBox.Show(item.Nombre); 

                ProcesarEMailMorosos(morosos);
            }
        }

        private void ProcesarEMailMorosos(IEnumerable<AlumnoMoroso> morosos)
        {
            foreach (var item in morosos)
            {
                EMail eMail = new EMail();
                if (!string.IsNullOrEmpty(item.EMail))
                {
                    eMail.To = item.EMail;
                    eMail.Body = EMailRepository.ArmarBodyEMailHtml(item.Apellido, item.Nombre, item.Documento, item.Carrera,
                                                item.Curso, item.CuotasAdeudadas, item.ImporteDeuda);
                    eMail.Subject = "Notificación de deuda - Instituto San Martín de Porres";
                    EMailRepository.EnviarEMail(eMail);
                }

            }
            morosos = null;
        }

        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgvDatos.Columns)
            {
                c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            dgvDatos.Columns[0].HeaderText = "Plan Pago";
            dgvDatos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            dgvDatos.Columns[1].HeaderText = "Carrera";
            dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[2].HeaderText = "Curso";
            dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[3].HeaderText = "Apellido";
            dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[4].HeaderText = "Nombre";
            dgvDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[5].HeaderText = "Documento";
            dgvDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[6].HeaderText = "EMail";
            dgvDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDatos.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[7].HeaderText = "Importe";
            dgvDatos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[8].HeaderText = "Cuotas Adeudadas";
            dgvDatos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }
        
    }
}
