namespace SMPorres.Forms.MenuItems
{
    partial class frmAsignarUsuariosyGruposAMenuItems
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
            this.lbAsignados = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelAsignados = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbSinAsignar = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbUsuarios = new System.Windows.Forms.RadioButton();
            this.rbGrupos = new System.Windows.Forms.RadioButton();
            this.lbMenu = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(680, 468);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbAsignados);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(343, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 462);
            this.panel2.TabIndex = 1;
            // 
            // lbAsignados
            // 
            this.lbAsignados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAsignados.FormattingEnabled = true;
            this.lbAsignados.Location = new System.Drawing.Point(37, 22);
            this.lbAsignados.Name = "lbAsignados";
            this.lbAsignados.Size = new System.Drawing.Size(297, 440);
            this.lbAsignados.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelAsignados);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(37, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(297, 22);
            this.panel3.TabIndex = 5;
            // 
            // labelAsignados
            // 
            this.labelAsignados.AutoSize = true;
            this.labelAsignados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAsignados.Location = new System.Drawing.Point(6, 5);
            this.labelAsignados.Name = "labelAsignados";
            this.labelAsignados.Size = new System.Drawing.Size(65, 13);
            this.labelAsignados.TabIndex = 1;
            this.labelAsignados.Text = "Asignados";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.btnQuitar);
            this.panel6.Controls.Add(this.btnAsignar);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(37, 462);
            this.panel6.TabIndex = 4;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Image = global::SMPorres.Properties.Resources.control_rewind_blue;
            this.btnQuitar.Location = new System.Drawing.Point(5, 205);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(27, 27);
            this.btnQuitar.TabIndex = 1;
            this.btnQuitar.UseVisualStyleBackColor = true;
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            // 
            // btnAsignar
            // 
            this.btnAsignar.Image = global::SMPorres.Properties.Resources.control_fastforward_blue;
            this.btnAsignar.Location = new System.Drawing.Point(5, 150);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(27, 27);
            this.btnAsignar.TabIndex = 0;
            this.btnAsignar.UseVisualStyleBackColor = true;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lbSinAsignar, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbMenu, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(334, 462);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // lbSinAsignar
            // 
            this.lbSinAsignar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSinAsignar.FormattingEnabled = true;
            this.lbSinAsignar.Location = new System.Drawing.Point(3, 249);
            this.lbSinAsignar.Name = "lbSinAsignar";
            this.lbSinAsignar.Size = new System.Drawing.Size(328, 210);
            this.lbSinAsignar.TabIndex = 1;
            this.lbSinAsignar.DoubleClick += new System.EventHandler(this.lbSinAsignar_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbUsuarios);
            this.panel1.Controls.Add(this.rbGrupos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 219);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(328, 24);
            this.panel1.TabIndex = 2;
            // 
            // rbUsuarios
            // 
            this.rbUsuarios.AutoSize = true;
            this.rbUsuarios.Location = new System.Drawing.Point(151, 4);
            this.rbUsuarios.Name = "rbUsuarios";
            this.rbUsuarios.Size = new System.Drawing.Size(66, 17);
            this.rbUsuarios.TabIndex = 1;
            this.rbUsuarios.TabStop = true;
            this.rbUsuarios.Text = "Usuarios";
            this.rbUsuarios.UseVisualStyleBackColor = true;
            this.rbUsuarios.CheckedChanged += new System.EventHandler(this.rbUsuarios_CheckedChanged);
            // 
            // rbGrupos
            // 
            this.rbGrupos.AutoSize = true;
            this.rbGrupos.Location = new System.Drawing.Point(6, 3);
            this.rbGrupos.Name = "rbGrupos";
            this.rbGrupos.Size = new System.Drawing.Size(59, 17);
            this.rbGrupos.TabIndex = 0;
            this.rbGrupos.TabStop = true;
            this.rbGrupos.Text = "Grupos";
            this.rbGrupos.UseVisualStyleBackColor = true;
            this.rbGrupos.CheckedChanged += new System.EventHandler(this.rbGrupos_CheckedChanged);
            // 
            // lbMenu
            // 
            this.lbMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMenu.FormattingEnabled = true;
            this.lbMenu.Location = new System.Drawing.Point(3, 3);
            this.lbMenu.Name = "lbMenu";
            this.lbMenu.Size = new System.Drawing.Size(328, 210);
            this.lbMenu.TabIndex = 3;
            // 
            // frmAsignarUsuariosyGruposAMenuItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 468);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmAsignarUsuariosyGruposAMenuItems";
            this.Text = "Asignar permisos a Usuarios y Grupos";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox lbAsignados;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelAsignados;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.ListBox lbSinAsignar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbUsuarios;
        private System.Windows.Forms.RadioButton rbGrupos;
        private System.Windows.Forms.ListBox lbMenu;
    }
}