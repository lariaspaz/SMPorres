﻿using SMPorres.Lib.AppForms;

namespace SMPorres.Forms
{
    public partial class frmReporte : FormBase
    {
        public frmReporte(string título, object reportSource)
        {
            InitializeComponent();
            this.Text = título;
            crystalReportViewer1.SelectionMode = CrystalDecisions.Windows.Forms.SelectionMode.None;            
            crystalReportViewer1.ReportSource = reportSource;
        }        
    }
}
