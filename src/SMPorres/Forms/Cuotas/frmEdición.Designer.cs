namespace SMPorres.Forms.Cuotas
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
            this.txtPeríodo = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtVtoCuota = new System.Windows.Forms.DateTimePicker();
            this.lblVtoCuota = new System.Windows.Forms.Label();
            this.txtNroCuota = new CustomLibrary.ComponentModel.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.panel2.Controls.Add(this.txtPeríodo);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dtVtoCuota);
            this.panel2.Controls.Add(this.lblVtoCuota);
            this.panel2.Controls.Add(this.txtNroCuota);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(321, 100);
            this.panel2.TabIndex = 1;
            // 
            // txtPeríodo
            // 
            this.txtPeríodo.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPeríodo.IntValue = ((long)(0));
            this.txtPeríodo.Location = new System.Drawing.Point(111, 64);
            this.txtPeríodo.Name = "txtPeríodo";
            this.txtPeríodo.Size = new System.Drawing.Size(50, 20);
            this.txtPeríodo.TabIndex = 2;
            this.txtPeríodo.Text = "0";
            this.txtPeríodo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ciclo lectivo:";
            // 
            // dtVtoCuota
            // 
            this.dtVtoCuota.Location = new System.Drawing.Point(111, 38);
            this.dtVtoCuota.Name = "dtVtoCuota";
            this.dtVtoCuota.Size = new System.Drawing.Size(200, 20);
            this.dtVtoCuota.TabIndex = 1;
            // 
            // lblVtoCuota
            // 
            this.lblVtoCuota.AutoSize = true;
            this.lblVtoCuota.Location = new System.Drawing.Point(45, 45);
            this.lblVtoCuota.Name = "lblVtoCuota";
            this.lblVtoCuota.Size = new System.Drawing.Size(60, 13);
            this.lblVtoCuota.TabIndex = 13;
            this.lblVtoCuota.Text = "Vto. Cuota:";
            // 
            // txtNroCuota
            // 
            this.txtNroCuota.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNroCuota.IntValue = ((long)(0));
            this.txtNroCuota.Location = new System.Drawing.Point(111, 9);
            this.txtNroCuota.Name = "txtNroCuota";
            this.txtNroCuota.Size = new System.Drawing.Size(50, 20);
            this.txtNroCuota.TabIndex = 0;
            this.txtNroCuota.Text = "0";
            this.txtNroCuota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nº Cuota:";
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(327, 141);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnAceptar);
            this.panel1.Controls.Add(this.bevel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 106);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(239, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(158, 7);
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
            this.bevel1.Size = new System.Drawing.Size(327, 23);
            this.bevel1.TabIndex = 3;
            this.bevel1.Text = "bevel1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmEdición
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(327, 141);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private CustomLibrary.ComponentModel.Bevel bevel1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private CustomLibrary.ComponentModel.NumericTextBox txtNroCuota;
        private System.Windows.Forms.Label lblVtoCuota;
        private System.Windows.Forms.DateTimePicker dtVtoCuota;
        private CustomLibrary.ComponentModel.NumericTextBox txtPeríodo;
        private System.Windows.Forms.Label label1;
    }
}