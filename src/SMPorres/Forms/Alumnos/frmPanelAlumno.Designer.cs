﻿namespace SMPorres.Forms.Alumnos
{
    partial class frmPanelAlumno
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtNroDocumento = new CustomLibrary.ComponentModel.NumericTextBox();
            this.btnBuscarAlumno = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvCursos = new CustomLibrary.ComponentModel.CustomDataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvPlanesPago = new CustomLibrary.ComponentModel.CustomDataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvPagos = new CustomLibrary.ComponentModel.CustomDataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnGenerarPlanPago = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCursos)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanesPago)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº Documento: ";
            // 
            // txtNroDocumento
            // 
            this.txtNroDocumento.DecValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNroDocumento.IntValue = ((long)(0));
            this.txtNroDocumento.Location = new System.Drawing.Point(103, 8);
            this.txtNroDocumento.Name = "txtNroDocumento";
            this.txtNroDocumento.Size = new System.Drawing.Size(99, 20);
            this.txtNroDocumento.TabIndex = 1;
            this.txtNroDocumento.Text = "0";
            this.txtNroDocumento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNroDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNroDocumento_KeyDown);
            // 
            // btnBuscarAlumno
            // 
            this.btnBuscarAlumno.Image = global::SMPorres.Properties.Resources.zoom;
            this.btnBuscarAlumno.Location = new System.Drawing.Point(205, 6);
            this.btnBuscarAlumno.Name = "btnBuscarAlumno";
            this.btnBuscarAlumno.Size = new System.Drawing.Size(23, 23);
            this.btnBuscarAlumno.TabIndex = 2;
            this.btnBuscarAlumno.UseVisualStyleBackColor = true;
            this.btnBuscarAlumno.Click += new System.EventHandler(this.btnBuscarAlumno_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Nombre: ";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(307, 9);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(321, 20);
            this.txtNombre.TabIndex = 4;
            this.txtNombre.TabStop = false;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(634, 6);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 5;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnConsultar);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnBuscarAlumno);
            this.panel1.Controls.Add(this.txtNroDocumento);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(719, 39);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.btnEliminar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(719, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = global::SMPorres.Properties.Resources.add;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(23, 22);
            this.btnNuevo.Text = "toolStripButton1";
            this.btnNuevo.ToolTipText = "Nuevo (Ctrl + N)";
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = global::SMPorres.Properties.Resources.page_white_edit;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(23, 22);
            this.btnEditar.Text = "toolStripButton2";
            this.btnEditar.ToolTipText = "Editar (Ctrl + F4)";
            // 
            // btnEliminar
            // 
            this.btnEliminar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEliminar.Image = global::SMPorres.Properties.Resources.cross;
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(23, 22);
            this.btnEliminar.Text = "toolStripButton3";
            this.btnEliminar.ToolTipText = "Eliminar (Ctrl + Delete)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSalir
            // 
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = global::SMPorres.Properties.Resources.door;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(23, 22);
            this.btnSalir.Text = "toolStripButton4";
            this.btnSalir.ToolTipText = "Salir (Escape)";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvCursos);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 64);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(719, 100);
            this.panel2.TabIndex = 2;
            // 
            // dgvCursos
            // 
            this.dgvCursos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCursos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCursos.EvenRowColor = System.Drawing.Color.Empty;
            this.dgvCursos.Location = new System.Drawing.Point(0, 23);
            this.dgvCursos.Name = "dgvCursos";
            this.dgvCursos.OddRowColor = System.Drawing.Color.Empty;
            this.dgvCursos.Size = new System.Drawing.Size(719, 77);
            this.dgvCursos.TabIndex = 1;
            this.dgvCursos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCursos_DataBindingComplete);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(719, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cursos en los que se inscribió el alumno";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Controls.Add(this.toolStrip2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 164);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(719, 193);
            this.panel3.TabIndex = 3;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(719, 168);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvPlanesPago);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(711, 142);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Planes de pago";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvPlanesPago
            // 
            this.dgvPlanesPago.AllowUserToAddRows = false;
            this.dgvPlanesPago.AllowUserToDeleteRows = false;
            this.dgvPlanesPago.AllowUserToResizeRows = false;
            this.dgvPlanesPago.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPlanesPago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlanesPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPlanesPago.EvenRowColor = System.Drawing.Color.Empty;
            this.dgvPlanesPago.Location = new System.Drawing.Point(3, 3);
            this.dgvPlanesPago.MultiSelect = false;
            this.dgvPlanesPago.Name = "dgvPlanesPago";
            this.dgvPlanesPago.OddRowColor = System.Drawing.Color.AliceBlue;
            this.dgvPlanesPago.ReadOnly = true;
            this.dgvPlanesPago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlanesPago.Size = new System.Drawing.Size(705, 136);
            this.dgvPlanesPago.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvPagos);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(711, 142);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pagos";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvPagos
            // 
            this.dgvPagos.AllowUserToAddRows = false;
            this.dgvPagos.AllowUserToDeleteRows = false;
            this.dgvPagos.AllowUserToResizeRows = false;
            this.dgvPagos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPagos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPagos.EvenRowColor = System.Drawing.Color.Empty;
            this.dgvPagos.Location = new System.Drawing.Point(3, 3);
            this.dgvPagos.MultiSelect = false;
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.OddRowColor = System.Drawing.Color.AliceBlue;
            this.dgvPagos.ReadOnly = true;
            this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagos.Size = new System.Drawing.Size(705, 136);
            this.dgvPagos.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGenerarPlanPago,
            this.toolStripSeparator2});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(719, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnGenerarPlanPago
            // 
            this.btnGenerarPlanPago.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGenerarPlanPago.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerarPlanPago.Name = "btnGenerarPlanPago";
            this.btnGenerarPlanPago.Size = new System.Drawing.Size(124, 22);
            this.btnGenerarPlanPago.Text = "Generar Plan de Pago";
            this.btnGenerarPlanPago.Click += new System.EventHandler(this.btnGenerarPlanPago_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // frmPanelAlumno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 357);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "frmPanelAlumno";
            this.Text = "Panel del alumno";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPanelAlumno_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCursos)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlanesPago)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuscarAlumno;
        private CustomLibrary.ComponentModel.NumericTextBox txtNroDocumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private CustomLibrary.ComponentModel.CustomDataGridView dgvCursos;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private CustomLibrary.ComponentModel.CustomDataGridView dgvPlanesPago;
        private CustomLibrary.ComponentModel.CustomDataGridView dgvPagos;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnGenerarPlanPago;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}