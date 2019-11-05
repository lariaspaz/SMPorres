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
/*
    1 Técnico Superior en Instrumentación Quirúrgica
    2 Técnico Superior en Radiología
    3 Técnico Superior en Hemoterapia
    4 Trabajador Social
    5 Técnico Superior en Laboratorio de Análisis Clínicos
*/
        private void ConsultarDatos()
        {
            short uCuota = CuotasRepository.ÚltimaCuota();
            short idCarrera = 1;
            List<AlumnoMoroso> am = (List < AlumnoMoroso >) AlumnosMorososRepository.ObtenerAlumnosMorosos(uCuota, idCarrera);

            foreach (var item in am)
            {
                item.ImporteDeuda = Convert.ToDecimal(PagosRepository.ObtenerDetallePago(item.IdPago, DateTime.Today).ImportePagado);
            }

            var morosos = (from m in am
                           group m by new { m.IdPlanPago, m.Carrera, m.Curso, m.Apellido, m.Nombre, m.Documento, m.EMail }
                           into g
                           select new AlumnoMoroso
                           {
                               IdPlanPago = g.Key.IdPlanPago,
                               Carrera = g.Key.Carrera,
                               Curso = g.Key.Curso,
                               Apellido = g.Key.Apellido,
                               Nombre = g.Key.Nombre,
                               Documento = g.Key.Documento,
                               EMail = g.Key.EMail,
                               ImporteDeuda = g.Sum(s => s.ImporteDeuda),
                               CuotasAdeudadas = g.Count()
                           });
            //dgvDatos.SetDataSource(morosos);
            enviarEMailMorosos(morosos);
        }

        private void enviarEMailMorosos(IEnumerable<AlumnoMoroso> morosos)
        {
            //controlar en Log los mensajes que se intenta enviar
            foreach (var item in morosos)
            {
                if (string.IsNullOrEmpty(item.EMail)) return;
                
            }
        }

        private decimal ObtenerDeuda(int idPlanPago, Int16 cuotaReferencia)
        {
            
            var cuotas = AlumnosMorososRepository.ObtenerCuotasAdeudadas(idPlanPago, cuotaReferencia);

            decimal deuda = 0;
            foreach (var i in cuotas)
            {
                deuda += Convert.ToDecimal(PagosRepository.ObtenerDetallePago(i.Id, DateTime.Today).ImportePagado);                
            }

            return deuda;
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

            //dgvDatos.Columns[7].HeaderText = "IdPago";
            //dgvDatos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDatos.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //
            //dgvDatos.Columns[8].HeaderText = "NroCuota";
            //dgvDatos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvDatos.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[7].HeaderText = "Importe";
            dgvDatos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDatos.Columns[8].HeaderText = "Cuotas Adeudadas";
            dgvDatos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }

        private Int32 IdPlanPagoSeleccionado()
        {
            try
            {
                int rowindex = dgvDatos.CurrentCell.RowIndex;
                var id = (Int32)dgvDatos.Rows[rowindex].Cells[0].Value;
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void btnEMail_Click(object sender, EventArgs e)
        {
            List<string> destinatarios = new List<string>()
            {
                "hernan_jhc@hotmail.com",
                "hernan.jhc@gmail.com"
            };


            //
            // se crea el mensaje
            //
            string body = "holaaaaa";

            //using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MailSendTest.MailBody.txt"))
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    body = reader.ReadToEnd();
            //}

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("hernan.jhc@gmail.com"),
                Body = body,
                Subject = "Mail Test",
                IsBodyHtml = false
            };


            //
            // se asignan los destinatarios
            //
            foreach (string item in destinatarios)
            {
                mail.To.Add(new MailAddress(item));
            }


            //
            // se define el smtp
            //
            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("hernan.jhc@gmail.com", "..."),
                EnableSsl = true
            };


            smtp.Send(mail);
        }

        private void btnCalcularDeuda_Click(object sender, EventArgs e)
        {
            var idpp = IdPlanPagoSeleccionado();
            var deuda = ObtenerDeuda(idpp,7);
            MessageBox.Show("La deuda es de: " + deuda.ToString(), "Deuda");
        }
    }
}
