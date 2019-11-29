namespace SMPorres.Forms.Cursos
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
            this.dtPagoAdelantadoHasta = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCuota3 = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCuota2 = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCuota1 = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDescuentoPagoAdelantado = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ckEstado = new System.Windows.Forms.CheckBox();
            this.cbModalidad = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtImporteMatrícula = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImporteCuota = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCarreras = new System.Windows.Forms.ComboBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.bevel1 = new CustomLibrary.ComponentModel.Bevel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bevel2 = new SMPorres.Lib.Bevel();
            this.bevel3 = new SMPorres.Lib.Bevel();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.bevel3);
            this.panel2.Controls.Add(this.bevel2);
            this.panel2.Controls.Add(this.dtPagoAdelantadoHasta);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtCuota3);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtCuota2);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtCuota1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.txtDescuentoPagoAdelantado);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.ckEstado);
            this.panel2.Controls.Add(this.cbModalidad);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtImporteMatrícula);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtImporteCuota);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbCarreras);
            this.panel2.Controls.Add(this.txtNombre);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(441, 362);
            this.panel2.TabIndex = 1;
            // 
            // dtPagoAdelantadoHasta
            // 
            this.dtPagoAdelantadoHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPagoAdelantadoHasta.Location = new System.Drawing.Point(322, 164);
            this.dtPagoAdelantadoHasta.Name = "dtPagoAdelantadoHasta";
            this.dtPagoAdelantadoHasta.Size = new System.Drawing.Size(99, 20);
            this.dtPagoAdelantadoHasta.TabIndex = 27;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(281, 167);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Hasta:";
            // 
            // txtCuota3
            // 
            this.txtCuota3.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtCuota3.Digits = 2;
            this.txtCuota3.IntValue = ((long)(0));
            this.txtCuota3.Location = new System.Drawing.Point(160, 242);
            this.txtCuota3.Name = "txtCuota3";
            this.txtCuota3.Size = new System.Drawing.Size(100, 20);
            this.txtCuota3.TabIndex = 24;
            this.txtCuota3.Text = "0,00";
            this.txtCuota3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(110, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Cuota 3:";
            // 
            // txtCuota2
            // 
            this.txtCuota2.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtCuota2.Digits = 2;
            this.txtCuota2.IntValue = ((long)(0));
            this.txtCuota2.Location = new System.Drawing.Point(160, 216);
            this.txtCuota2.Name = "txtCuota2";
            this.txtCuota2.Size = new System.Drawing.Size(100, 20);
            this.txtCuota2.TabIndex = 22;
            this.txtCuota2.Text = "0,00";
            this.txtCuota2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(110, 219);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Cuota 2:";
            // 
            // txtCuota1
            // 
            this.txtCuota1.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtCuota1.Digits = 2;
            this.txtCuota1.IntValue = ((long)(0));
            this.txtCuota1.Location = new System.Drawing.Point(160, 190);
            this.txtCuota1.Name = "txtCuota1";
            this.txtCuota1.Size = new System.Drawing.Size(100, 20);
            this.txtCuota1.TabIndex = 20;
            this.txtCuota1.Text = "0,00";
            this.txtCuota1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(110, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Cuota 1:";
            // 
            // txtDescuentoPagoAdelantado
            // 
            this.txtDescuentoPagoAdelantado.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtDescuentoPagoAdelantado.Digits = 2;
            this.txtDescuentoPagoAdelantado.IntValue = ((long)(0));
            this.txtDescuentoPagoAdelantado.Location = new System.Drawing.Point(160, 164);
            this.txtDescuentoPagoAdelantado.Name = "txtDescuentoPagoAdelantado";
            this.txtDescuentoPagoAdelantado.Size = new System.Drawing.Size(100, 20);
            this.txtDescuentoPagoAdelantado.TabIndex = 18;
            this.txtDescuentoPagoAdelantado.Text = "0,00";
            this.txtDescuentoPagoAdelantado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Descuento pago adelantado:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Cuota";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Matrícula";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(103, 321);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Estado: ";
            // 
            // ckEstado
            // 
            this.ckEstado.AutoSize = true;
            this.ckEstado.Location = new System.Drawing.Point(160, 321);
            this.ckEstado.Name = "ckEstado";
            this.ckEstado.Size = new System.Drawing.Size(73, 17);
            this.ckEstado.TabIndex = 14;
            this.ckEstado.Text = "Habilitado";
            this.ckEstado.UseVisualStyleBackColor = true;
            // 
            // cbModalidad
            // 
            this.cbModalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModalidad.FormattingEnabled = true;
            this.cbModalidad.Location = new System.Drawing.Point(160, 64);
            this.cbModalidad.Name = "cbModalidad";
            this.cbModalidad.Size = new System.Drawing.Size(261, 21);
            this.cbModalidad.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Modalidad:";
            // 
            // txtImporteMatrícula
            // 
            this.txtImporteMatrícula.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtImporteMatrícula.Digits = 2;
            this.txtImporteMatrícula.IntValue = ((long)(0));
            this.txtImporteMatrícula.Location = new System.Drawing.Point(160, 138);
            this.txtImporteMatrícula.Name = "txtImporteMatrícula";
            this.txtImporteMatrícula.Size = new System.Drawing.Size(100, 20);
            this.txtImporteMatrícula.TabIndex = 4;
            this.txtImporteMatrícula.Text = "0,00";
            this.txtImporteMatrícula.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Importe:";
            // 
            // txtImporteCuota
            // 
            this.txtImporteCuota.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            131072});
            this.txtImporteCuota.Digits = 2;
            this.txtImporteCuota.IntValue = ((long)(0));
            this.txtImporteCuota.Location = new System.Drawing.Point(160, 295);
            this.txtImporteCuota.Name = "txtImporteCuota";
            this.txtImporteCuota.Size = new System.Drawing.Size(100, 20);
            this.txtImporteCuota.TabIndex = 5;
            this.txtImporteCuota.Text = "0,00";
            this.txtImporteCuota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Importe:";
            // 
            // cbCarreras
            // 
            this.cbCarreras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCarreras.FormattingEnabled = true;
            this.cbCarreras.Location = new System.Drawing.Point(160, 37);
            this.cbCarreras.Name = "cbCarreras";
            this.cbCarreras.Size = new System.Drawing.Size(261, 21);
            this.cbCarreras.TabIndex = 2;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(160, 11);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(261, 20);
            this.txtNombre.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pertenece a la carrera:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre: ";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(447, 403);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.bevel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 368);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(341, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(260, 7);
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
            this.bevel1.Size = new System.Drawing.Size(447, 23);
            this.bevel1.TabIndex = 3;
            this.bevel1.Text = "bevel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // bevel2
            // 
            this.bevel2.Location = new System.Drawing.Point(100, 97);
            this.bevel2.Name = "bevel2";
            this.bevel2.Size = new System.Drawing.Size(321, 23);
            this.bevel2.TabIndex = 28;
            this.bevel2.Text = "bevel2";
            // 
            // bevel3
            // 
            this.bevel3.Location = new System.Drawing.Point(67, 268);
            this.bevel3.Name = "bevel3";
            this.bevel3.Size = new System.Drawing.Size(354, 23);
            this.bevel3.TabIndex = 29;
            this.bevel3.Text = "bevel3";
            // 
            // frmEdición
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(447, 403);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmEdición";
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
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private CustomLibrary.ComponentModel.Bevel bevel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cbCarreras;
        private CustomLibrary.ComponentModel.NumericTextBox txtImporteCuota;
        private System.Windows.Forms.Label label3;
        private CustomLibrary.ComponentModel.NumericTextBox txtImporteMatrícula;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbModalidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckEstado;
        private System.Windows.Forms.DateTimePicker dtPagoAdelantadoHasta;
        private System.Windows.Forms.Label label13;
        private CustomLibrary.ComponentModel.NumericTextBox txtCuota3;
        private System.Windows.Forms.Label label12;
        private CustomLibrary.ComponentModel.NumericTextBox txtCuota2;
        private System.Windows.Forms.Label label11;
        private CustomLibrary.ComponentModel.NumericTextBox txtCuota1;
        private System.Windows.Forms.Label label10;
        private CustomLibrary.ComponentModel.NumericTextBox txtDescuentoPagoAdelantado;
        private System.Windows.Forms.Label label9;
        private Lib.Bevel bevel3;
        private Lib.Bevel bevel2;
    }
}