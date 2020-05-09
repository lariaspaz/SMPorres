namespace SMPorres.Forms
{
    partial class frmConfiguración
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtEndpointAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDiasVtoPagoTermino = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCicloLectivo = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInterésPorMora = new CustomLibrary.ComponentModel.NumericTextBox();
            this.txtDescPagoTérmino = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bevel2 = new CustomLibrary.ComponentModel.Bevel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.bevel1 = new CustomLibrary.ComponentModel.Bevel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtEndpointAddress);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtDiasVtoPagoTermino);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtCicloLectivo);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtInterésPorMora);
            this.panel2.Controls.Add(this.txtDescPagoTérmino);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.bevel2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(309, 189);
            this.panel2.TabIndex = 1;
            // 
            // txtEndpointAddress
            // 
            this.txtEndpointAddress.Location = new System.Drawing.Point(20, 152);
            this.txtEndpointAddress.Name = "txtEndpointAddress";
            this.txtEndpointAddress.Size = new System.Drawing.Size(272, 20);
            this.txtEndpointAddress.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Dirección del servicio web del sitio de consultas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Días a vencer pago a término:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiasVtoPagoTermino
            // 
            this.txtDiasVtoPagoTermino.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDiasVtoPagoTermino.IntValue = ((long)(0));
            this.txtDiasVtoPagoTermino.Location = new System.Drawing.Point(192, 89);
            this.txtDiasVtoPagoTermino.Name = "txtDiasVtoPagoTermino";
            this.txtDiasVtoPagoTermino.Size = new System.Drawing.Size(100, 20);
            this.txtDiasVtoPagoTermino.TabIndex = 4;
            this.txtDiasVtoPagoTermino.Text = "0";
            this.txtDiasVtoPagoTermino.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ciclo Lectivo: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCicloLectivo
            // 
            this.txtCicloLectivo.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtCicloLectivo.IntValue = ((long)(0));
            this.txtCicloLectivo.Location = new System.Drawing.Point(192, 63);
            this.txtCicloLectivo.Name = "txtCicloLectivo";
            this.txtCicloLectivo.Size = new System.Drawing.Size(100, 20);
            this.txtCicloLectivo.TabIndex = 3;
            this.txtCicloLectivo.Text = "0";
            this.txtCicloLectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Interés Mensual por Mora:  %";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtInterésPorMora
            // 
            this.txtInterésPorMora.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtInterésPorMora.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtInterésPorMora.Digits = 2;
            this.txtInterésPorMora.Enabled = false;
            this.txtInterésPorMora.IntValue = ((long)(0));
            this.txtInterésPorMora.Location = new System.Drawing.Point(192, 37);
            this.txtInterésPorMora.Name = "txtInterésPorMora";
            this.txtInterésPorMora.Size = new System.Drawing.Size(100, 20);
            this.txtInterésPorMora.TabIndex = 2;
            this.txtInterésPorMora.Text = "0,00";
            this.txtInterésPorMora.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDescPagoTérmino
            // 
            this.txtDescPagoTérmino.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDescPagoTérmino.Digits = 2;
            this.txtDescPagoTérmino.IntValue = ((long)(0));
            this.txtDescPagoTérmino.Location = new System.Drawing.Point(192, 11);
            this.txtDescPagoTérmino.Name = "txtDescPagoTérmino";
            this.txtDescPagoTérmino.Size = new System.Drawing.Size(100, 20);
            this.txtDescPagoTérmino.TabIndex = 1;
            this.txtDescPagoTérmino.Text = "0,00";
            this.txtDescPagoTérmino.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descuento por Pago a Término: %";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bevel2
            // 
            this.bevel2.BevelStyle = CustomLibrary.ComponentModel.BevelStyle.Raised;
            this.bevel2.BevelType = CustomLibrary.ComponentModel.BevelType.Frame;
            this.bevel2.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevel2.Location = new System.Drawing.Point(9, 128);
            this.bevel2.Name = "bevel2";
            this.bevel2.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevel2.Size = new System.Drawing.Size(291, 53);
            this.bevel2.TabIndex = 14;
            this.bevel2.Text = "bevel2";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(315, 230);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.bevel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 195);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(228, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(147, 7);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "&Grabar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // bevel1
            // 
            this.bevel1.BevelStyle = CustomLibrary.ComponentModel.BevelStyle.Raised;
            this.bevel1.BevelType = CustomLibrary.ComponentModel.BevelType.TopLine;
            this.bevel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bevel1.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevel1.Location = new System.Drawing.Point(0, 0);
            this.bevel1.Margin = new System.Windows.Forms.Padding(0);
            this.bevel1.Name = "bevel1";
            this.bevel1.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevel1.Size = new System.Drawing.Size(315, 23);
            this.bevel1.TabIndex = 3;
            this.bevel1.Text = "bevel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmConfiguración
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(315, 230);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmConfiguración";
            this.Text = "frmEdición";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private CustomLibrary.ComponentModel.Bevel bevel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private CustomLibrary.ComponentModel.NumericTextBox txtDescPagoTérmino;
        private CustomLibrary.ComponentModel.NumericTextBox txtInterésPorMora;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private CustomLibrary.ComponentModel.NumericTextBox txtCicloLectivo;
        private System.Windows.Forms.Label label4;
        private CustomLibrary.ComponentModel.NumericTextBox txtDiasVtoPagoTermino;
        private System.Windows.Forms.TextBox txtEndpointAddress;
        private System.Windows.Forms.Label label5;
        private CustomLibrary.ComponentModel.Bevel bevel2;
    }
}