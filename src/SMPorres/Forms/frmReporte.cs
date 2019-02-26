using CrystalDecisions.CrystalReports.Engine;
using SMPorres.Lib.AppForms;

namespace SMPorres.Forms
{
    public partial class frmReporte : FormBase
    {
        public frmReporte(object reporte)
        {
            InitializeComponent();
            crystalReportViewer1.SelectionMode = CrystalDecisions.Windows.Forms.SelectionMode.None;
            crystalReportViewer1.ReportSource = reporte;
        }

        public frmReporte(object reporte, string título) : this(reporte)
        {
            this.Text = título;
            var rd = (ReportDocument)reporte;
            rd.SummaryInfo.ReportTitle = título;
        }

        public frmReporte(object reporte, string título, string subTítulo) : this(reporte, título)
        {
            var rd = (ReportDocument)reporte;
            rd.DataDefinition.FormulaFields["Subtítulo"].Text = "'" + subTítulo + "'";
        }
    }
}
