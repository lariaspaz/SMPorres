namespace Utilidades
{
    partial class frmConfigurarCadenaConexión
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClaveRegistroGrabar = new System.Windows.Forms.TextBox();
            this.btnEncriptarGrabar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUnencryptedConnectionString = new System.Windows.Forms.TextBox();
            this.btnDesencriptar = new System.Windows.Forms.Button();
            this.txtClaveRegistroLeer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cadena de conexión:";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(12, 25);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(346, 20);
            this.txtConnectionString.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Clave del registro adonde grabar:";
            // 
            // txtClaveRegistroGrabar
            // 
            this.txtClaveRegistroGrabar.Location = new System.Drawing.Point(12, 64);
            this.txtClaveRegistroGrabar.Name = "txtClaveRegistroGrabar";
            this.txtClaveRegistroGrabar.Size = new System.Drawing.Size(346, 20);
            this.txtClaveRegistroGrabar.TabIndex = 3;
            this.txtClaveRegistroGrabar.Text = "HKEY_LOCAL_MACHINE\\SOFTWARE\\SMP\\Cs";
            // 
            // btnEncriptarGrabar
            // 
            this.btnEncriptarGrabar.Location = new System.Drawing.Point(255, 90);
            this.btnEncriptarGrabar.Name = "btnEncriptarGrabar";
            this.btnEncriptarGrabar.Size = new System.Drawing.Size(103, 29);
            this.btnEncriptarGrabar.TabIndex = 4;
            this.btnEncriptarGrabar.Text = "Encriptar y grabar";
            this.btnEncriptarGrabar.UseVisualStyleBackColor = true;
            this.btnEncriptarGrabar.Click += new System.EventHandler(this.btnEncriptarGrabar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cadena de conexión desencriptada:";
            // 
            // txtUnencryptedConnectionString
            // 
            this.txtUnencryptedConnectionString.Location = new System.Drawing.Point(12, 183);
            this.txtUnencryptedConnectionString.Name = "txtUnencryptedConnectionString";
            this.txtUnencryptedConnectionString.Size = new System.Drawing.Size(346, 20);
            this.txtUnencryptedConnectionString.TabIndex = 6;
            // 
            // btnDesencriptar
            // 
            this.btnDesencriptar.Location = new System.Drawing.Point(255, 209);
            this.btnDesencriptar.Name = "btnDesencriptar";
            this.btnDesencriptar.Size = new System.Drawing.Size(103, 29);
            this.btnDesencriptar.TabIndex = 7;
            this.btnDesencriptar.Text = "Desencriptar";
            this.btnDesencriptar.UseVisualStyleBackColor = true;
            this.btnDesencriptar.Click += new System.EventHandler(this.btnDesencriptar_Click);
            // 
            // txtClaveRegistroLeer
            // 
            this.txtClaveRegistroLeer.Location = new System.Drawing.Point(12, 144);
            this.txtClaveRegistroLeer.Name = "txtClaveRegistroLeer";
            this.txtClaveRegistroLeer.Size = new System.Drawing.Size(346, 20);
            this.txtClaveRegistroLeer.TabIndex = 9;
            this.txtClaveRegistroLeer.Text = "HKEY_LOCAL_MACHINE\\SOFTWARE\\SMP\\Cs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Clave del registro de donde leer:";
            // 
            // frmConfigurarCadenaConexión
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 250);
            this.Controls.Add(this.txtClaveRegistroLeer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnDesencriptar);
            this.Controls.Add(this.txtUnencryptedConnectionString);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEncriptarGrabar);
            this.Controls.Add(this.txtClaveRegistroGrabar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label1);
            this.Name = "frmConfigurarCadenaConexión";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurar cadena de conexión";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClaveRegistroGrabar;
        private System.Windows.Forms.Button btnEncriptarGrabar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUnencryptedConnectionString;
        private System.Windows.Forms.Button btnDesencriptar;
        private System.Windows.Forms.TextBox txtClaveRegistroLeer;
        private System.Windows.Forms.Label label4;
    }
}

