namespace Utilidades
{
    partial class frmConsultasWeb
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtEndpoint = new System.Windows.Forms.TextBox();
            this.btnVerificarConexion = new System.Windows.Forms.Button();
            this.btnLimpiarBD = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dirección del Endpoint:";
            // 
            // txtEndpoint
            // 
            this.txtEndpoint.Location = new System.Drawing.Point(12, 25);
            this.txtEndpoint.Name = "txtEndpoint";
            this.txtEndpoint.Size = new System.Drawing.Size(332, 20);
            this.txtEndpoint.TabIndex = 1;
            // 
            // btnVerificarConexion
            // 
            this.btnVerificarConexion.Location = new System.Drawing.Point(50, 51);
            this.btnVerificarConexion.Name = "btnVerificarConexion";
            this.btnVerificarConexion.Size = new System.Drawing.Size(114, 23);
            this.btnVerificarConexion.TabIndex = 2;
            this.btnVerificarConexion.Text = "Verificar conexión";
            this.btnVerificarConexion.UseVisualStyleBackColor = true;
            this.btnVerificarConexion.Click += new System.EventHandler(this.btnVerificarConexion_Click);
            // 
            // btnLimpiarBD
            // 
            this.btnLimpiarBD.Location = new System.Drawing.Point(170, 51);
            this.btnLimpiarBD.Name = "btnLimpiarBD";
            this.btnLimpiarBD.Size = new System.Drawing.Size(131, 23);
            this.btnLimpiarBD.TabIndex = 3;
            this.btnLimpiarBD.Text = "Limpiar base de datos";
            this.btnLimpiarBD.UseVisualStyleBackColor = true;
            this.btnLimpiarBD.Click += new System.EventHandler(this.btnLimpiarBD_Click);
            // 
            // frmConsultasWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 86);
            this.Controls.Add(this.btnLimpiarBD);
            this.Controls.Add(this.btnVerificarConexion);
            this.Controls.Add(this.txtEndpoint);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultasWeb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tester del Web Service de Consultas Web";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEndpoint;
        private System.Windows.Forms.Button btnVerificarConexion;
        private System.Windows.Forms.Button btnLimpiarBD;
    }
}