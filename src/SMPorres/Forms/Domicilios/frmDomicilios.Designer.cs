namespace SMPorres.Forms.Domicilios
{
    partial class frmDomicilios
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPadre = new System.Windows.Forms.Label();
            this.txtHijo = new System.Windows.Forms.TextBox();
            this.lblHijo = new System.Windows.Forms.Label();
            this.txtPadre = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(385, 142);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.btnAceptar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 110);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 29);
            this.panel2.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(295, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(214, 3);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "&Grabar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPadre);
            this.panel1.Controls.Add(this.txtHijo);
            this.panel1.Controls.Add(this.lblHijo);
            this.panel1.Controls.Add(this.txtPadre);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 101);
            this.panel1.TabIndex = 0;
            // 
            // lblPadre
            // 
            this.lblPadre.AutoSize = true;
            this.lblPadre.Location = new System.Drawing.Point(7, 10);
            this.lblPadre.Name = "lblPadre";
            this.lblPadre.Size = new System.Drawing.Size(35, 13);
            this.lblPadre.TabIndex = 4;
            this.lblPadre.Text = "Padre";
            // 
            // txtHijo
            // 
            this.txtHijo.Location = new System.Drawing.Point(9, 71);
            this.txtHijo.Name = "txtHijo";
            this.txtHijo.Size = new System.Drawing.Size(358, 20);
            this.txtHijo.TabIndex = 3;
            // 
            // lblHijo
            // 
            this.lblHijo.AutoSize = true;
            this.lblHijo.Location = new System.Drawing.Point(9, 55);
            this.lblHijo.Name = "lblHijo";
            this.lblHijo.Size = new System.Drawing.Size(25, 13);
            this.lblHijo.TabIndex = 2;
            this.lblHijo.Text = "Hijo";
            // 
            // txtPadre
            // 
            this.txtPadre.Enabled = false;
            this.txtPadre.Location = new System.Drawing.Point(9, 27);
            this.txtPadre.Name = "txtPadre";
            this.txtPadre.Size = new System.Drawing.Size(358, 20);
            this.txtPadre.TabIndex = 1;
            // 
            // frmDomicilios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 142);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "frmDomicilios";
            this.Text = "frmDomicilios";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDomicilios_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtPadre;
        private System.Windows.Forms.TextBox txtHijo;
        private System.Windows.Forms.Label lblHijo;
        private System.Windows.Forms.Label lblPadre;
    }
}