namespace SMPorres.Forms.Alumnos
{
    partial class frmListado
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
            this.dgvDatos = new CustomLibrary.ComponentModel.CustomDataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.bntPrint = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bevel1 = new CustomLibrary.ComponentModel.Bevel();
            this.txtSexo = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEMail = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFechaNacimiento = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBarrio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDepartamento = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProvincia = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatos.EvenRowColor = System.Drawing.Color.Empty;
            this.dgvDatos.Location = new System.Drawing.Point(3, 28);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.OddRowColor = System.Drawing.Color.AliceBlue;
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(843, 261);
            this.dgvDatos.TabIndex = 2;
            this.dgvDatos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDatos_DataBindingComplete);
            this.dgvDatos.SelectionChanged += new System.EventHandler(this.dgvDatos_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.bntPrint,
            this.btnEliminar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(849, 25);
            this.toolStrip1.TabIndex = 3;
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
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
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
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // bntPrint
            // 
            this.bntPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bntPrint.Image = global::SMPorres.Properties.Resources.printer;
            this.bntPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bntPrint.Name = "bntPrint";
            this.bntPrint.Size = new System.Drawing.Size(23, 22);
            this.bntPrint.Text = "toolStripButton3";
            this.bntPrint.ToolTipText = "Eliminar (Ctrl + Delete)";
            this.bntPrint.Click += new System.EventHandler(this.bntPrint_Click);
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
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvDatos, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.27088F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.72912F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(849, 468);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bevel1);
            this.panel1.Controls.Add(this.txtSexo);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtEstado);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtEMail);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtFechaNacimiento);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.txtDocumento);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtApellido);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtNombre);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtDireccion);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtBarrio);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtLocalidad);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtDepartamento);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtProvincia);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 295);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 170);
            this.panel1.TabIndex = 4;
            // 
            // bevel1
            // 
            this.bevel1.BevelStyle = CustomLibrary.ComponentModel.BevelStyle.Raised;
            this.bevel1.BevelType = CustomLibrary.ComponentModel.BevelType.TopLine;
            this.bevel1.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bevel1.Location = new System.Drawing.Point(80, 98);
            this.bevel1.Name = "bevel1";
            this.bevel1.ShadowColor = System.Drawing.SystemColors.ButtonShadow;
            this.bevel1.Size = new System.Drawing.Size(616, 5);
            this.bevel1.TabIndex = 28;
            this.bevel1.Text = "bevel1";
            // 
            // txtSexo
            // 
            this.txtSexo.BackColor = System.Drawing.SystemColors.Window;
            this.txtSexo.Location = new System.Drawing.Point(80, 62);
            this.txtSexo.Name = "txtSexo";
            this.txtSexo.ReadOnly = true;
            this.txtSexo.Size = new System.Drawing.Size(150, 20);
            this.txtSexo.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(40, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 26;
            this.label14.Text = "Sexo:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(15, 90);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Domicilio";
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.SystemColors.Window;
            this.txtEstado.Location = new System.Drawing.Point(536, 37);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(150, 20);
            this.txtEstado.TabIndex = 24;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(487, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Estado:";
            // 
            // txtEMail
            // 
            this.txtEMail.BackColor = System.Drawing.SystemColors.Window;
            this.txtEMail.Location = new System.Drawing.Point(298, 36);
            this.txtEMail.Name = "txtEMail";
            this.txtEMail.ReadOnly = true;
            this.txtEMail.Size = new System.Drawing.Size(150, 20);
            this.txtEMail.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(256, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "EMail:";
            // 
            // txtFechaNacimiento
            // 
            this.txtFechaNacimiento.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaNacimiento.Location = new System.Drawing.Point(80, 36);
            this.txtFechaNacimiento.Name = "txtFechaNacimiento";
            this.txtFechaNacimiento.ReadOnly = true;
            this.txtFechaNacimiento.Size = new System.Drawing.Size(150, 20);
            this.txtFechaNacimiento.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Fec. Nac.:";
            // 
            // txtDocumento
            // 
            this.txtDocumento.BackColor = System.Drawing.SystemColors.Window;
            this.txtDocumento.Location = new System.Drawing.Point(536, 10);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.ReadOnly = true;
            this.txtDocumento.Size = new System.Drawing.Size(150, 20);
            this.txtDocumento.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(465, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Documento:";
            // 
            // txtApellido
            // 
            this.txtApellido.BackColor = System.Drawing.SystemColors.Window;
            this.txtApellido.Location = new System.Drawing.Point(298, 10);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.ReadOnly = true;
            this.txtApellido.Size = new System.Drawing.Size(150, 20);
            this.txtApellido.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(245, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Apellido:";
            // 
            // txtNombre
            // 
            this.txtNombre.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombre.Location = new System.Drawing.Point(80, 9);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(150, 20);
            this.txtNombre.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Nombre:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.SystemColors.Window;
            this.txtDireccion.Location = new System.Drawing.Point(80, 109);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(150, 20);
            this.txtDireccion.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Dirección:";
            // 
            // txtBarrio
            // 
            this.txtBarrio.BackColor = System.Drawing.SystemColors.Window;
            this.txtBarrio.Location = new System.Drawing.Point(298, 135);
            this.txtBarrio.Name = "txtBarrio";
            this.txtBarrio.ReadOnly = true;
            this.txtBarrio.Size = new System.Drawing.Size(150, 20);
            this.txtBarrio.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(255, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Barrio:";
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.BackColor = System.Drawing.SystemColors.Window;
            this.txtLocalidad.Location = new System.Drawing.Point(80, 135);
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.ReadOnly = true;
            this.txtLocalidad.Size = new System.Drawing.Size(149, 20);
            this.txtLocalidad.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Localidad:";
            // 
            // txtDepartamento
            // 
            this.txtDepartamento.BackColor = System.Drawing.SystemColors.Window;
            this.txtDepartamento.Location = new System.Drawing.Point(536, 109);
            this.txtDepartamento.Name = "txtDepartamento";
            this.txtDepartamento.ReadOnly = true;
            this.txtDepartamento.Size = new System.Drawing.Size(150, 20);
            this.txtDepartamento.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(453, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Departamento:";
            // 
            // txtProvincia
            // 
            this.txtProvincia.BackColor = System.Drawing.SystemColors.Window;
            this.txtProvincia.Location = new System.Drawing.Point(298, 109);
            this.txtProvincia.Name = "txtProvincia";
            this.txtProvincia.ReadOnly = true;
            this.txtProvincia.Size = new System.Drawing.Size(149, 20);
            this.txtProvincia.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Provincia:";
            // 
            // frmListado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 468);
            this.Controls.Add(this.tableLayoutPanel1);
            this.KeyPreview = true;
            this.Name = "frmListado";
            this.Text = "Alumnos";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListado_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomLibrary.ComponentModel.CustomDataGridView dgvDatos;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBarrio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDepartamento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProvincia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton bntPrint;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFechaNacimiento;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEMail;
        private System.Windows.Forms.Label label11;
        private CustomLibrary.ComponentModel.Bevel bevel1;
        private System.Windows.Forms.TextBox txtSexo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label label12;
    }
}