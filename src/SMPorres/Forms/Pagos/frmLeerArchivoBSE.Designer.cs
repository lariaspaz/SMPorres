namespace SMPorres.Forms.Pagos
{
    partial class frmLeerArchivoBSE
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
            this.lPath = new System.Windows.Forms.Label();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvArchivoBSE = new System.Windows.Forms.DataGridView();
            this.btnLeer = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivoBSE)).BeginInit();
            this.SuspendLayout();
            // 
            // lPath
            // 
            this.lPath.AutoSize = true;
            this.lPath.Location = new System.Drawing.Point(206, 38);
            this.lPath.Name = "lPath";
            this.lPath.Size = new System.Drawing.Size(16, 13);
            this.lPath.TabIndex = 1;
            this.lPath.Text = "...";
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(108, 28);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(75, 23);
            this.btnExaminar.TabIndex = 2;
            this.btnExaminar.Text = "Examinar";
            this.btnExaminar.UseVisualStyleBackColor = true;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLeer);
            this.groupBox1.Controls.Add(this.btnExaminar);
            this.groupBox1.Controls.Add(this.lPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 78);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccionar archivo";
            // 
            // dgvArchivoBSE
            // 
            this.dgvArchivoBSE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArchivoBSE.Location = new System.Drawing.Point(12, 278);
            this.dgvArchivoBSE.Name = "dgvArchivoBSE";
            this.dgvArchivoBSE.Size = new System.Drawing.Size(647, 167);
            this.dgvArchivoBSE.TabIndex = 4;
            // 
            // btnLeer
            // 
            this.btnLeer.Location = new System.Drawing.Point(492, 49);
            this.btnLeer.Name = "btnLeer";
            this.btnLeer.Size = new System.Drawing.Size(75, 23);
            this.btnLeer.TabIndex = 3;
            this.btnLeer.Text = "Leer";
            this.btnLeer.UseVisualStyleBackColor = true;
            this.btnLeer.Click += new System.EventHandler(this.btnLeer_Click);
            // 
            // frmLeerArchivoBSE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 414);
            this.Controls.Add(this.dgvArchivoBSE);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmLeerArchivoBSE";
            this.Text = "frmLeerArchivoBSE";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArchivoBSE)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lPath;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLeer;
        private System.Windows.Forms.DataGridView dgvArchivoBSE;
    }
}