namespace SMPorres.Forms.Alumnos
{
    partial class frmAsignarAlumnosACursos
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCicloLectivo = new System.Windows.Forms.Label();
            this.cbCursos = new System.Windows.Forms.ComboBox();
            this.cbCarreras = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbSinAsignar = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbAsignados = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.07527F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.92473F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 465);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCicloLectivo);
            this.panel1.Controls.Add(this.cbCursos);
            this.panel1.Controls.Add(this.cbCarreras);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 92);
            this.panel1.TabIndex = 0;
            // 
            // lblCicloLectivo
            // 
            this.lblCicloLectivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCicloLectivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCicloLectivo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblCicloLectivo.Location = new System.Drawing.Point(194, 65);
            this.lblCicloLectivo.Name = "lblCicloLectivo";
            this.lblCicloLectivo.Size = new System.Drawing.Size(201, 18);
            this.lblCicloLectivo.TabIndex = 5;
            this.lblCicloLectivo.Text = "Ciclo Lectivo 2.019";
            this.lblCicloLectivo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbCursos
            // 
            this.cbCursos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCursos.FormattingEnabled = true;
            this.cbCursos.Location = new System.Drawing.Point(62, 36);
            this.cbCursos.Name = "cbCursos";
            this.cbCursos.Size = new System.Drawing.Size(333, 21);
            this.cbCursos.TabIndex = 4;
            this.cbCursos.SelectedValueChanged += new System.EventHandler(this.cbCursos_SelectedValueChanged);
            // 
            // cbCarreras
            // 
            this.cbCarreras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCarreras.FormattingEnabled = true;
            this.cbCarreras.Location = new System.Drawing.Point(62, 9);
            this.cbCarreras.Name = "cbCarreras";
            this.cbCarreras.Size = new System.Drawing.Size(333, 21);
            this.cbCarreras.TabIndex = 3;
            this.cbCarreras.SelectedValueChanged += new System.EventHandler(this.cbCarreras_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Carrera: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Curso: ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 101);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbSinAsignar);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel4);
            this.splitContainer1.Size = new System.Drawing.Size(589, 361);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Paint);
            // 
            // lbSinAsignar
            // 
            this.lbSinAsignar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSinAsignar.FormattingEnabled = true;
            this.lbSinAsignar.Location = new System.Drawing.Point(0, 22);
            this.lbSinAsignar.Name = "lbSinAsignar";
            this.lbSinAsignar.Size = new System.Drawing.Size(271, 337);
            this.lbSinAsignar.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 22);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Sin asignar";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbAsignados);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(310, 359);
            this.panel4.TabIndex = 2;
            // 
            // lbAsignados
            // 
            this.lbAsignados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbAsignados.FormattingEnabled = true;
            this.lbAsignados.Location = new System.Drawing.Point(37, 22);
            this.lbAsignados.Name = "lbAsignados";
            this.lbAsignados.Size = new System.Drawing.Size(273, 337);
            this.lbAsignados.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(37, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(273, 22);
            this.panel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Asignados";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Control;
            this.panel6.Controls.Add(this.btnSalir);
            this.panel6.Controls.Add(this.btnQuitar);
            this.panel6.Controls.Add(this.btnAsignar);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(37, 359);
            this.panel6.TabIndex = 0;
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
            // btnSalir
            // 
            this.btnSalir.Image = global::SMPorres.Properties.Resources.door;
            this.btnSalir.Location = new System.Drawing.Point(3, 324);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(27, 27);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // frmAsignarAlumnosACursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnSalir;
            this.ClientSize = new System.Drawing.Size(595, 465);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmAsignarAlumnosACursos";
            this.Text = "Asignar alumnos a cursos";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCursos;
        private System.Windows.Forms.ComboBox cbCarreras;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.ListBox lbSinAsignar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbAsignados;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCicloLectivo;
        private System.Windows.Forms.Button btnSalir;
    }
}