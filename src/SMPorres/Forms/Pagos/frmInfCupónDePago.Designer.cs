namespace SMPorres.Forms.Pagos
{
    partial class frmInfCupónDePago
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ckTodas = new System.Windows.Forms.CheckBox();
            this.txtDescripción = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFechaPago = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.bevel1 = new CustomLibrary.ComponentModel.Bevel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(356, 130);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCancelar);
            this.panel3.Controls.Add(this.btnAceptar);
            this.panel3.Controls.Add(this.bevel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 95);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(356, 35);
            this.panel3.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(181, 6);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(100, 6);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "&Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ckTodas);
            this.panel2.Controls.Add(this.txtDescripción);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dtFechaPago);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 89);
            this.panel2.TabIndex = 1;
            // 
            // ckTodas
            // 
            this.ckTodas.AutoSize = true;
            this.ckTodas.Location = new System.Drawing.Point(82, 61);
            this.ckTodas.Name = "ckTodas";
            this.ckTodas.Size = new System.Drawing.Size(223, 17);
            this.ckTodas.TabIndex = 14;
            this.ckTodas.Text = "Imprimir todas las cuotas impagas del plan";
            this.ckTodas.UseVisualStyleBackColor = true;
            this.ckTodas.CheckedChanged += new System.EventHandler(this.ckTodas_CheckedChanged);
            // 
            // txtDescripción
            // 
            this.txtDescripción.BackColor = System.Drawing.Color.AliceBlue;
            this.txtDescripción.Location = new System.Drawing.Point(82, 9);
            this.txtDescripción.Name = "txtDescripción";
            this.txtDescripción.ReadOnly = true;
            this.txtDescripción.Size = new System.Drawing.Size(256, 20);
            this.txtDescripción.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Cuota: ";
            // 
            // dtFechaPago
            // 
            this.dtFechaPago.Location = new System.Drawing.Point(82, 35);
            this.dtFechaPago.Name = "dtFechaPago";
            this.dtFechaPago.Size = new System.Drawing.Size(200, 20);
            this.dtFechaPago.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pagar hasta:";
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
            this.bevel1.Size = new System.Drawing.Size(356, 23);
            this.bevel1.TabIndex = 4;
            this.bevel1.Text = "bevel1";
            // 
            // frmInfCupónDePago
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(356, 130);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmInfCupónDePago";
            this.Text = "Impresión de Cuotas";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFechaPago;
        private System.Windows.Forms.TextBox txtDescripción;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckTodas;
        private CustomLibrary.ComponentModel.Bevel bevel1;
    }
}