namespace Punto_de_venta_new_era
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
        	System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        	this.dgvDetalles = new System.Windows.Forms.DataGridView();
        	this.colClave = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colPUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.colImporte = new System.Windows.Forms.DataGridViewTextBoxColumn();
        	this.panelTotales = new System.Windows.Forms.Panel();
        	this.lblNumVenta = new System.Windows.Forms.Label();
        	this.lblSubtotalT = new System.Windows.Forms.Label();
        	this.lblSubtotalV = new System.Windows.Forms.Label();
        	this.lblIvaT = new System.Windows.Forms.Label();
        	this.lblIvaV = new System.Windows.Forms.Label();
        	this.lblTotalT = new System.Windows.Forms.Label();
        	this.lblTotalV = new System.Windows.Forms.Label();
        	this.panelBotones = new System.Windows.Forms.Panel();
        	this.pictureBox5 = new System.Windows.Forms.PictureBox();
        	this.pictureBox4 = new System.Windows.Forms.PictureBox();
        	this.pictureBox3 = new System.Windows.Forms.PictureBox();
        	this.pictureBox2 = new System.Windows.Forms.PictureBox();
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.btnDescuento = new System.Windows.Forms.Button();
        	this.btnBorrarItem = new System.Windows.Forms.Button();
        	this.btnCantidad = new System.Windows.Forms.Button();
        	this.btnCliente = new System.Windows.Forms.Button();
        	this.btnPagar = new System.Windows.Forms.Button();
        	this.pnlTop = new System.Windows.Forms.Panel();
        	this.lblDetallesCompra = new System.Windows.Forms.Label();
        	((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
        	this.panelTotales.SuspendLayout();
        	this.panelBotones.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.pnlTop.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// dgvDetalles
        	// 
        	this.dgvDetalles.AllowUserToAddRows = false;
        	dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
        	this.dgvDetalles.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        	this.dgvDetalles.BackgroundColor = System.Drawing.Color.White;
        	this.dgvDetalles.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        	this.dgvDetalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
        	        	        	this.colClave,
        	        	        	this.colProducto,
        	        	        	this.colPUnitario,
        	        	        	this.colCantidad,
        	        	        	this.colIVA,
        	        	        	this.colImporte});
        	this.dgvDetalles.Location = new System.Drawing.Point(12, 95);
        	this.dgvDetalles.Name = "dgvDetalles";
        	this.dgvDetalles.RowHeadersWidth = 40;
        	this.dgvDetalles.Size = new System.Drawing.Size(730, 337);
        	this.dgvDetalles.TabIndex = 0;
        	// 
        	// colClave
        	// 
        	this.colClave.HeaderText = "CLAVE";
        	this.colClave.Name = "colClave";
        	this.colClave.Width = 120;
        	// 
        	// colProducto
        	// 
        	this.colProducto.HeaderText = "PRODUCTO";
        	this.colProducto.Name = "colProducto";
        	this.colProducto.Width = 200;
        	// 
        	// colPUnitario
        	// 
        	this.colPUnitario.HeaderText = "P. UNITARIO";
        	this.colPUnitario.Name = "colPUnitario";
        	this.colPUnitario.Width = 90;
        	// 
        	// colCantidad
        	// 
        	this.colCantidad.HeaderText = "CANTIDAD";
        	this.colCantidad.Name = "colCantidad";
        	this.colCantidad.Width = 80;
        	// 
        	// colIVA
        	// 
        	this.colIVA.HeaderText = "IVA";
        	this.colIVA.Name = "colIVA";
        	this.colIVA.Width = 80;
        	// 
        	// colImporte
        	// 
        	this.colImporte.HeaderText = "IMPORTE";
        	this.colImporte.Name = "colImporte";
        	this.colImporte.Width = 90;
        	// 
        	// panelTotales
        	// 
        	this.panelTotales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
        	this.panelTotales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        	this.panelTotales.Controls.Add(this.lblNumVenta);
        	this.panelTotales.Controls.Add(this.lblSubtotalT);
        	this.panelTotales.Controls.Add(this.lblSubtotalV);
        	this.panelTotales.Controls.Add(this.lblIvaT);
        	this.panelTotales.Controls.Add(this.lblIvaV);
        	this.panelTotales.Controls.Add(this.lblTotalT);
        	this.panelTotales.Controls.Add(this.lblTotalV);
        	this.panelTotales.Location = new System.Drawing.Point(12, 440);
        	this.panelTotales.Name = "panelTotales";
        	this.panelTotales.Size = new System.Drawing.Size(730, 100);
        	this.panelTotales.TabIndex = 1;
        	// 
        	// lblNumVenta
        	// 
        	this.lblNumVenta.BackColor = System.Drawing.SystemColors.ButtonFace;
        	this.lblNumVenta.ForeColor = System.Drawing.SystemColors.ButtonFace;
        	this.lblNumVenta.Location = new System.Drawing.Point(620, 10);
        	this.lblNumVenta.Name = "lblNumVenta";
        	this.lblNumVenta.Size = new System.Drawing.Size(90, 25);
        	this.lblNumVenta.TabIndex = 0;
        	this.lblNumVenta.Text = "Venta #1024";
        	this.lblNumVenta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// lblSubtotalT
        	// 
        	this.lblSubtotalT.AutoSize = true;
        	this.lblSubtotalT.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        	this.lblSubtotalT.Location = new System.Drawing.Point(15, 50);
        	this.lblSubtotalT.Name = "lblSubtotalT";
        	this.lblSubtotalT.Size = new System.Drawing.Size(174, 32);
        	this.lblSubtotalT.TabIndex = 1;
        	this.lblSubtotalT.Text = "SUBTOTAL:";
        	// 
        	// lblSubtotalV
        	// 
        	this.lblSubtotalV.AutoSize = true;
        	this.lblSubtotalV.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        	this.lblSubtotalV.ForeColor = System.Drawing.Color.ForestGreen;
        	this.lblSubtotalV.Location = new System.Drawing.Point(183, 50);
        	this.lblSubtotalV.Name = "lblSubtotalV";
        	this.lblSubtotalV.Size = new System.Drawing.Size(68, 32);
        	this.lblSubtotalV.TabIndex = 2;
        	this.lblSubtotalV.Text = "$0.0";
        	// 
        	// lblIvaT
        	// 
        	this.lblIvaT.AutoSize = true;
        	this.lblIvaT.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        	this.lblIvaT.Location = new System.Drawing.Point(270, 50);
        	this.lblIvaT.Name = "lblIvaT";
        	this.lblIvaT.Size = new System.Drawing.Size(147, 32);
        	this.lblIvaT.TabIndex = 3;
        	this.lblIvaT.Text = "IVA (16%):";
        	// 
        	// lblIvaV
        	// 
        	this.lblIvaV.AutoSize = true;
        	this.lblIvaV.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        	this.lblIvaV.ForeColor = System.Drawing.Color.ForestGreen;
        	this.lblIvaV.Location = new System.Drawing.Point(411, 50);
        	this.lblIvaV.Name = "lblIvaV";
        	this.lblIvaV.Size = new System.Drawing.Size(68, 32);
        	this.lblIvaV.TabIndex = 4;
        	this.lblIvaV.Text = "$0.0";
        	// 
        	// lblTotalT
        	// 
        	this.lblTotalT.AutoSize = true;
        	this.lblTotalT.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        	this.lblTotalT.Location = new System.Drawing.Point(510, 50);
        	this.lblTotalT.Name = "lblTotalT";
        	this.lblTotalT.Size = new System.Drawing.Size(116, 32);
        	this.lblTotalT.TabIndex = 5;
        	this.lblTotalT.Text = "TOTAL:";
        	// 
        	// lblTotalV
        	// 
        	this.lblTotalV.AutoSize = true;
        	this.lblTotalV.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
        	this.lblTotalV.ForeColor = System.Drawing.Color.ForestGreen;
        	this.lblTotalV.Location = new System.Drawing.Point(620, 50);
        	this.lblTotalV.Name = "lblTotalV";
        	this.lblTotalV.Size = new System.Drawing.Size(68, 32);
        	this.lblTotalV.TabIndex = 6;
        	this.lblTotalV.Text = "$0.0";
        	// 
        	// panelBotones
        	// 
        	this.panelBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(150)))), ((int)(((byte)(210)))));
        	this.panelBotones.Controls.Add(this.pictureBox5);
        	this.panelBotones.Controls.Add(this.pictureBox4);
        	this.panelBotones.Controls.Add(this.pictureBox3);
        	this.panelBotones.Controls.Add(this.pictureBox2);
        	this.panelBotones.Controls.Add(this.pictureBox1);
        	this.panelBotones.Controls.Add(this.btnDescuento);
        	this.panelBotones.Controls.Add(this.btnBorrarItem);
        	this.panelBotones.Controls.Add(this.btnCantidad);
        	this.panelBotones.Controls.Add(this.btnCliente);
        	this.panelBotones.Controls.Add(this.btnPagar);
        	this.panelBotones.Location = new System.Drawing.Point(748, 12);
        	this.panelBotones.Name = "panelBotones";
        	this.panelBotones.Size = new System.Drawing.Size(315, 528);
        	this.panelBotones.TabIndex = 2;
        	// 
        	// pictureBox5
        	// 
        	this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
        	this.pictureBox5.Location = new System.Drawing.Point(15, 391);
        	this.pictureBox5.Name = "pictureBox5";
        	this.pictureBox5.Size = new System.Drawing.Size(76, 50);
        	this.pictureBox5.TabIndex = 9;
        	this.pictureBox5.TabStop = false;
        	// 
        	// pictureBox4
        	// 
        	this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
        	this.pictureBox4.Location = new System.Drawing.Point(31, 281);
        	this.pictureBox4.Name = "pictureBox4";
        	this.pictureBox4.Size = new System.Drawing.Size(40, 39);
        	this.pictureBox4.TabIndex = 8;
        	this.pictureBox4.TabStop = false;
        	// 
        	// pictureBox3
        	// 
        	this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
        	this.pictureBox3.Location = new System.Drawing.Point(37, 196);
        	this.pictureBox3.Name = "pictureBox3";
        	this.pictureBox3.Size = new System.Drawing.Size(33, 42);
        	this.pictureBox3.TabIndex = 7;
        	this.pictureBox3.TabStop = false;
        	// 
        	// pictureBox2
        	// 
        	this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
        	this.pictureBox2.Location = new System.Drawing.Point(31, 112);
        	this.pictureBox2.Name = "pictureBox2";
        	this.pictureBox2.Size = new System.Drawing.Size(39, 37);
        	this.pictureBox2.TabIndex = 6;
        	this.pictureBox2.TabStop = false;
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
        	this.pictureBox1.Location = new System.Drawing.Point(31, 33);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(38, 32);
        	this.pictureBox1.TabIndex = 5;
        	this.pictureBox1.TabStop = false;
        	// 
        	// btnDescuento
        	// 
        	this.btnDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(110)))), ((int)(((byte)(180)))));
        	this.btnDescuento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btnDescuento.Font = new System.Drawing.Font("Arial", 14F);
        	this.btnDescuento.ForeColor = System.Drawing.Color.White;
        	this.btnDescuento.Location = new System.Drawing.Point(75, 17);
        	this.btnDescuento.Name = "btnDescuento";
        	this.btnDescuento.Size = new System.Drawing.Size(200, 60);
        	this.btnDescuento.TabIndex = 0;
        	this.btnDescuento.Text = "Descuento";
        	this.btnDescuento.UseVisualStyleBackColor = false;
        	// 
        	// btnBorrarItem
        	// 
        	this.btnBorrarItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(110)))), ((int)(((byte)(180)))));
        	this.btnBorrarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btnBorrarItem.Font = new System.Drawing.Font("Arial", 14F);
        	this.btnBorrarItem.ForeColor = System.Drawing.Color.White;
        	this.btnBorrarItem.Location = new System.Drawing.Point(75, 102);
        	this.btnBorrarItem.Name = "btnBorrarItem";
        	this.btnBorrarItem.Size = new System.Drawing.Size(200, 60);
        	this.btnBorrarItem.TabIndex = 1;
        	this.btnBorrarItem.Text = "Borrar Ítem";
        	this.btnBorrarItem.UseVisualStyleBackColor = false;
        	// 
        	// btnCantidad
        	// 
        	this.btnCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(110)))), ((int)(((byte)(180)))));
        	this.btnCantidad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btnCantidad.Font = new System.Drawing.Font("Arial", 14F);
        	this.btnCantidad.ForeColor = System.Drawing.Color.White;
        	this.btnCantidad.Location = new System.Drawing.Point(75, 187);
        	this.btnCantidad.Name = "btnCantidad";
        	this.btnCantidad.Size = new System.Drawing.Size(200, 60);
        	this.btnCantidad.TabIndex = 2;
        	this.btnCantidad.Text = "Cantidad";
        	this.btnCantidad.UseVisualStyleBackColor = false;
        	// 
        	// btnCliente
        	// 
        	this.btnCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(110)))), ((int)(((byte)(180)))));
        	this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btnCliente.Font = new System.Drawing.Font("Arial", 14F);
        	this.btnCliente.ForeColor = System.Drawing.Color.White;
        	this.btnCliente.Location = new System.Drawing.Point(75, 270);
        	this.btnCliente.Name = "btnCliente";
        	this.btnCliente.Size = new System.Drawing.Size(200, 60);
        	this.btnCliente.TabIndex = 3;
        	this.btnCliente.Text = "Cliente";
        	this.btnCliente.UseVisualStyleBackColor = false;
        	// 
        	// btnPagar
        	// 
        	this.btnPagar.BackColor = System.Drawing.Color.ForestGreen;
        	this.btnPagar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.btnPagar.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
        	this.btnPagar.ForeColor = System.Drawing.Color.White;
        	this.btnPagar.Location = new System.Drawing.Point(97, 365);
        	this.btnPagar.Name = "btnPagar";
        	this.btnPagar.Size = new System.Drawing.Size(200, 110);
        	this.btnPagar.TabIndex = 4;
        	this.btnPagar.Text = "PAGAR";
        	this.btnPagar.UseVisualStyleBackColor = false;
        	// 
        	// pnlTop
        	// 
        	this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(150)))), ((int)(((byte)(210)))));
        	this.pnlTop.Controls.Add(this.lblDetallesCompra);
        	this.pnlTop.Location = new System.Drawing.Point(13, 12);
        	this.pnlTop.Name = "pnlTop";
        	this.pnlTop.Size = new System.Drawing.Size(729, 77);
        	this.pnlTop.TabIndex = 3;
        	// 
        	// lblDetallesCompra
        	// 
        	this.lblDetallesCompra.Font = new System.Drawing.Font("Britannic Bold", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.lblDetallesCompra.Location = new System.Drawing.Point(110, 17);
        	this.lblDetallesCompra.Name = "lblDetallesCompra";
        	this.lblDetallesCompra.Size = new System.Drawing.Size(600, 50);
        	this.lblDetallesCompra.TabIndex = 0;
        	this.lblDetallesCompra.Text = "DETALLES DE LA COMPRA";
        	// 
        	// MainForm
        	// 
        	this.BackColor = System.Drawing.Color.White;
        	this.ClientSize = new System.Drawing.Size(1059, 542);
        	this.Controls.Add(this.pnlTop);
        	this.Controls.Add(this.panelBotones);
        	this.Controls.Add(this.panelTotales);
        	this.Controls.Add(this.dgvDetalles);
        	this.Name = "MainForm";
        	this.Text = "Punto de Venta New Era";
        	((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
        	this.panelTotales.ResumeLayout(false);
        	this.panelTotales.PerformLayout();
        	this.panelBotones.ResumeLayout(false);
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.pnlTop.ResumeLayout(false);
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label lblDetallesCompra;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;

        private System.Windows.Forms.DataGridView dgvDetalles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImporte;
        private System.Windows.Forms.Panel panelTotales;
        private System.Windows.Forms.Label lblNumVenta;
        private System.Windows.Forms.Label lblSubtotalT;
        private System.Windows.Forms.Label lblSubtotalV;
        private System.Windows.Forms.Label lblIvaT;
        private System.Windows.Forms.Label lblIvaV;
        private System.Windows.Forms.Label lblTotalT;
        private System.Windows.Forms.Label lblTotalV;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnDescuento;
        private System.Windows.Forms.Button btnBorrarItem;
        private System.Windows.Forms.Button btnCantidad;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnPagar;
    }
}
