using CustomLibrary.Lib.Extensions;
using System.Windows.Forms;

namespace SMPorres.Lib.Validations
{
    public class FormValidations
    {
        private ErrorProvider _errorProvider;
        private Form _parent;

        public FormValidations(Form parent, ErrorProvider errorProvider)
        {
            this._errorProvider = errorProvider;
            this._parent = parent;
        }

        public bool Validar(TextBox txt, bool condición, string error)
        {
            bool result = true;
            if (!condición)
            {
                _errorProvider.SetError(txt, error);
                new ToolTip().ShowError(_parent, txt, error);
                result = false;
            }
            else
            {
                _errorProvider.SetError(txt, "");
            }
            return result;
        }
    }
}
