using SMPorres.Lib.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMPorres.Lib.AppForms
{
    public class FormBase : Form
    {
        protected FormValidations _validator;

        protected FormBase()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
        }

        protected void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected void ShowError(string message, Exception ex)
        {
            string error = ex.Message;
            while (error.Contains("inner exception"))
            {
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    error = ex.Message;
                }
                else
                {
                    break;
                }
            }
            ShowError(message + error);
        }
    }
}
