namespace SMPorres.Forms.Pagos
{
    partial class frmEdición
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
            this.txtDescBeca = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRecargoPorMora = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMediosPago = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.bevel1 = new CustomLibrary.ComponentModel.Bevel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtImporte = new CustomLibrary.ComponentModel.NumericTextBox();
            this.txtDescPagoATérmino = new CustomLibrary.ComponentModel.NumericTextBox();
            this.txtTotal = new CustomLibrary.ComponentModel.NumericTextBox();
            this.dtFechaPago = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCuota = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtCuota);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.dtFechaPago);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Controls.Add(this.txtDescPagoATérmino);
            this.panel2.Controls.Add(this.txtImporte);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtDescBeca);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtRecargoPorMora);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbMediosPago);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(354, 226);
            this.panel2.TabIndex = 1;
            // 
            // txtDescBeca
            // 
            this.txtDescBeca.BackColor = System.Drawing.SystemColors.Info;
            this.txtDescBeca.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDescBeca.Digits = 2;
            this.txtDescBeca.IntValue = ((long)(0));
            this.txtDescBeca.Location = new System.Drawing.Point(130, 89);
            this.txtDescBeca.Name = "txtDescBeca";
            this.txtDescBeca.ReadOnly = true;
            this.txtDescBeca.Size = new System.Drawing.Size(100, 20);
            this.txtDescBeca.TabIndex = 3;
            this.txtDescBeca.TabStop = false;
            this.txtDescBeca.Text = "0,00";
            this.txtDescBeca.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Descuento por beca:";
            // 
            // txtRecargoPorMora
            // 
            this.txtRecargoPorMora.BackColor = System.Drawing.SystemColors.Info;
            this.txtRecargoPorMora.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtRecargoPorMora.Digits = 2;
            this.txtRecargoPorMora.IntValue = ((long)(0));
            this.txtRecargoPorMora.Location = new System.Drawing.Point(130, 115);
            this.txtRecargoPorMora.Name = "txtRecargoPorMora";
            this.txtRecargoPorMora.ReadOnly = true;
            this.txtRecargoPorMora.Size = new System.Drawing.Size(100, 20);
            this.txtRecargoPorMora.TabIndex = 4;
            this.txtRecargoPorMora.TabStop = false;
            this.txtRecargoPorMora.Text = "0,00";
            this.txtRecargoPorMora.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Recargo por Mora:";
            // 
            // cbMediosPago
            // 
            this.cbMediosPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMediosPago.FormattingEnabled = true;
            this.cbMediosPago.Location = new System.Drawing.Point(130, 192);
            this.cbMediosPago.Name = "cbMediosPago";
            this.cbMediosPago.Size = new System.Drawing.Size(200, 21);
            this.cbMediosPago.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 195);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Medio de Pago:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cuota:";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 267);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.bevel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 232);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(258, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(177, 7);
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
            this.bevel1.Size = new System.Drawing.Size(360, 23);
            this.bevel1.TabIndex = 3;
            this.bevel1.Text = "bevel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Importe:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(39, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 29);
            this.label6.TabIndex = 7;
            this.label6.Text = "Descuento por Pago a Término:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(90, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Total:";
            // 
            // txtImporte
            // 
            this.txtImporte.BackColor = System.Drawing.SystemColors.Info;
            this.txtImporte.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtImporte.Digits = 2;
            this.txtImporte.IntValue = ((long)(0));
            this.txtImporte.Location = new System.Drawing.Point(130, 37);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.ReadOnly = true;
            this.txtImporte.Size = new System.Drawing.Size(100, 20);
            this.txtImporte.TabIndex = 1;
            this.txtImporte.TabStop = false;
            this.txtImporte.Text = "0,00";
            this.txtImporte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDescPagoATérmino
            // 
            this.txtDescPagoATérmino.BackColor = System.Drawing.SystemColors.Info;
            this.txtDescPagoATérmino.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDescPagoATérmino.Digits = 2;
            this.txtDescPagoATérmino.IntValue = ((long)(0));
            this.txtDescPagoATérmino.Location = new System.Drawing.Point(130, 63);
            this.txtDescPagoATérmino.Name = "txtDescPagoATérmino";
            this.txtDescPagoATérmino.ReadOnly = true;
            this.txtDescPagoATérmino.Size = new System.Drawing.Size(100, 20);
            this.txtDescPagoATérmino.TabIndex = 2;
            this.txtDescPagoATérmino.TabStop = false;
            this.txtDescPagoATérmino.Text = "0,00";
            this.txtDescPagoATérmino.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.AliceBlue;
            this.txtTotal.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtTotal.Digits = 2;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.IntValue = ((long)(0));
            this.txtTotal.Location = new System.Drawing.Point(130, 140);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 5;
            this.txtTotal.TabStop = false;
            this.txtTotal.Text = "0,00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtFechaPago
            // 
            this.dtFechaPago.Location = new System.Drawing.Point(130, 166);
            this.dtFechaPago.Name = "dtFechaPago";
            this.dtFechaPago.Size = new System.Drawing.Size(200, 20);
            this.dtFechaPago.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Fecha de Pago:";
            // 
            // txtCuota
            // 
            this.txtCuota.BackColor = System.Drawing.SystemColors.Info;
            this.txtCuota.Location = new System.Drawing.Point(130, 11);
            this.txtCuota.Name = "txtCuota";
            this.txtCuota.ReadOnly = true;
            this.txtCuota.Size = new System.Drawing.Size(100, 20);
            this.txtCuota.TabIndex = 0;
            this.txtCuota.TabStop = false;
            this.txtCuota.Text = "1 de 10";
            this.txtCuota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmEdición
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(360, 267);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmEdición";
            this.Text = "Pago de Cuotas";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private CustomLibrary.ComponentModel.Bevel bevel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cbMediosPago;
        private CustomLibrary.ComponentModel.NumericTextBox txtRecargoPorMora;
        private System.Windows.Forms.Label label3;
        private CustomLibrary.ComponentModel.NumericTextBox txtDescBeca;
        private System.Windows.Forms.Label label4;
        private CustomLibrary.ComponentModel.NumericTextBox txtTotal;
        private CustomLibrary.ComponentModel.NumericTextBox txtDescPagoATérmino;
        private CustomLibrary.ComponentModel.NumericTextBox txtImporte;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtFechaPago;
        private System.Windows.Forms.TextBox txtCuota;
    }
}