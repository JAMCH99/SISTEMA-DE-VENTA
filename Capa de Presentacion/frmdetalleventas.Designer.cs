
namespace Capa_de_Presentacion
{
    partial class frmdetalleventas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtusuario = new System.Windows.Forms.TextBox();
            this.txtTipodocumento = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtfechaventa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnclear = new FontAwesome.Sharp.IconButton();
            this.buscador = new FontAwesome.Sharp.IconButton();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtnumerobuscado = new System.Windows.Forms.TextBox();
            this.txtcliente = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtdocumentocliente = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtpago = new System.Windows.Forms.TextBox();
            this.txtcambio = new System.Windows.Forms.TextBox();
            this.txttotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btndownload = new FontAwesome.Sharp.IconButton();
            this.cmbnumerodocumento = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Producto,
            this.PrecioVenta,
            this.Cantidad,
            this.Subtotal});
            this.dataGridView1.Location = new System.Drawing.Point(232, 242);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(588, 179);
            this.dataGridView1.TabIndex = 58;
            // 
            // Producto
            // 
            this.Producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Producto.HeaderText = "Producto";
            this.Producto.MinimumWidth = 6;
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            this.Producto.Width = 87;
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "Precio Venta";
            this.PrecioVenta.MinimumWidth = 6;
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.MinimumWidth = 6;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // Subtotal
            // 
            this.Subtotal.HeaderText = "Sub Total";
            this.Subtotal.MinimumWidth = 6;
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtusuario);
            this.groupBox1.Controls.Add(this.txtTipodocumento);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtfechaventa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(232, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 74);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información Venta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 31;
            this.label4.Text = "Usuario";
            // 
            // txtusuario
            // 
            this.txtusuario.BackColor = System.Drawing.SystemColors.Menu;
            this.txtusuario.Location = new System.Drawing.Point(321, 42);
            this.txtusuario.Name = "txtusuario";
            this.txtusuario.Size = new System.Drawing.Size(142, 22);
            this.txtusuario.TabIndex = 30;
            // 
            // txtTipodocumento
            // 
            this.txtTipodocumento.BackColor = System.Drawing.SystemColors.Menu;
            this.txtTipodocumento.Location = new System.Drawing.Point(165, 43);
            this.txtTipodocumento.Name = "txtTipodocumento";
            this.txtTipodocumento.Size = new System.Drawing.Size(142, 22);
            this.txtTipodocumento.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(160, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 28;
            this.label5.Text = "Tipo Documento";
            // 
            // txtfechaventa
            // 
            this.txtfechaventa.BackColor = System.Drawing.SystemColors.Menu;
            this.txtfechaventa.Location = new System.Drawing.Point(9, 43);
            this.txtfechaventa.Name = "txtfechaventa";
            this.txtfechaventa.Size = new System.Drawing.Size(142, 22);
            this.txtfechaventa.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 22;
            this.label2.Text = "Fecha";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.White;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(215, 7);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(627, 464);
            this.label10.TabIndex = 52;
            this.label10.Text = "Detalle Venta";
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnclear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnclear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclear.IconChar = FontAwesome.Sharp.IconChar.Broom;
            this.btnclear.IconColor = System.Drawing.Color.Black;
            this.btnclear.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnclear.IconSize = 25;
            this.btnclear.Location = new System.Drawing.Point(774, 32);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(40, 31);
            this.btnclear.TabIndex = 66;
            this.btnclear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnclear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // buscador
            // 
            this.buscador.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buscador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buscador.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.buscador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buscador.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buscador.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            this.buscador.IconColor = System.Drawing.Color.Black;
            this.buscador.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.buscador.IconSize = 25;
            this.buscador.Location = new System.Drawing.Point(734, 32);
            this.buscador.Name = "buscador";
            this.buscador.Size = new System.Drawing.Size(39, 30);
            this.buscador.TabIndex = 65;
            this.buscador.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buscador.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buscador.UseVisualStyleBackColor = false;
            this.buscador.Click += new System.EventHandler(this.buscador_Click_1);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label12.Location = new System.Drawing.Point(477, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(111, 17);
            this.label12.TabIndex = 63;
            this.label12.Text = "Nro. Documento";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.txtnumerobuscado);
            this.groupBox3.Controls.Add(this.txtcliente);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtdocumentocliente);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(232, 150);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(588, 74);
            this.groupBox3.TabIndex = 54;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Información Venta";
            // 
            // txtnumerobuscado
            // 
            this.txtnumerobuscado.BackColor = System.Drawing.SystemColors.Menu;
            this.txtnumerobuscado.Location = new System.Drawing.Point(489, 43);
            this.txtnumerobuscado.Name = "txtnumerobuscado";
            this.txtnumerobuscado.Size = new System.Drawing.Size(82, 22);
            this.txtnumerobuscado.TabIndex = 30;
            // 
            // txtcliente
            // 
            this.txtcliente.BackColor = System.Drawing.SystemColors.Menu;
            this.txtcliente.Location = new System.Drawing.Point(165, 43);
            this.txtcliente.Name = "txtcliente";
            this.txtcliente.Size = new System.Drawing.Size(142, 22);
            this.txtcliente.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(169, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Nombre Cliente";
            // 
            // txtdocumentocliente
            // 
            this.txtdocumentocliente.BackColor = System.Drawing.SystemColors.Menu;
            this.txtdocumentocliente.Location = new System.Drawing.Point(9, 43);
            this.txtdocumentocliente.Name = "txtdocumentocliente";
            this.txtdocumentocliente.Size = new System.Drawing.Size(142, 22);
            this.txtdocumentocliente.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "Nro.Documento";
            // 
            // txtpago
            // 
            this.txtpago.BackColor = System.Drawing.SystemColors.Menu;
            this.txtpago.Location = new System.Drawing.Point(303, 437);
            this.txtpago.Name = "txtpago";
            this.txtpago.Size = new System.Drawing.Size(55, 22);
            this.txtpago.TabIndex = 68;
            // 
            // txtcambio
            // 
            this.txtcambio.BackColor = System.Drawing.SystemColors.Menu;
            this.txtcambio.Location = new System.Drawing.Point(460, 437);
            this.txtcambio.Name = "txtcambio";
            this.txtcambio.Size = new System.Drawing.Size(44, 22);
            this.txtcambio.TabIndex = 69;
            // 
            // txttotal
            // 
            this.txttotal.BackColor = System.Drawing.SystemColors.Menu;
            this.txttotal.Location = new System.Drawing.Point(594, 435);
            this.txttotal.Name = "txttotal";
            this.txttotal.Size = new System.Drawing.Size(44, 22);
            this.txttotal.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(252, 441);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 17);
            this.label1.TabIndex = 71;
            this.label1.Text = "Pago";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(396, 439);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 72;
            this.label3.Text = "Cambio";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(544, 439);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 73;
            this.label6.Text = "Total";
            // 
            // btndownload
            // 
            this.btndownload.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btndownload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btndownload.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btndownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndownload.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btndownload.IconChar = FontAwesome.Sharp.IconChar.Download;
            this.btndownload.IconColor = System.Drawing.Color.Black;
            this.btndownload.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btndownload.IconSize = 18;
            this.btndownload.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btndownload.Location = new System.Drawing.Point(668, 432);
            this.btndownload.Name = "btndownload";
            this.btndownload.Size = new System.Drawing.Size(152, 30);
            this.btndownload.TabIndex = 79;
            this.btndownload.Text = "DescargarPDF";
            this.btndownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btndownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btndownload.UseVisualStyleBackColor = false;
            this.btndownload.Click += new System.EventHandler(this.btndownload_Click);
            // 
            // cmbnumerodocumento
            // 
            this.cmbnumerodocumento.FormattingEnabled = true;
            this.cmbnumerodocumento.Location = new System.Drawing.Point(611, 37);
            this.cmbnumerodocumento.Name = "cmbnumerodocumento";
            this.cmbnumerodocumento.Size = new System.Drawing.Size(117, 24);
            this.cmbnumerodocumento.TabIndex = 80;
            // 
            // frmdetalleventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1098, 553);
            this.Controls.Add(this.cmbnumerodocumento);
            this.Controls.Add(this.btndownload);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txttotal);
            this.Controls.Add(this.txtcambio);
            this.Controls.Add(this.txtpago);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.buscador);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmdetalleventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmdetalleventas";
            this.Load += new System.EventHandler(this.frmdetalleventas_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtfechaventa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private FontAwesome.Sharp.IconButton btnclear;
        private FontAwesome.Sharp.IconButton buscador;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtusuario;
        private System.Windows.Forms.TextBox txtTipodocumento;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtnumerobuscado;
        private System.Windows.Forms.TextBox txtcliente;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtdocumentocliente;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtpago;
        private System.Windows.Forms.TextBox txtcambio;
        private System.Windows.Forms.TextBox txttotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private FontAwesome.Sharp.IconButton btndownload;
        private System.Windows.Forms.ComboBox cmbnumerodocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
    }
}