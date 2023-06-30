using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Capa_Entidad;
using Capa_de_Presentacion.Utilidades;
using Capa_de_Presentacion.ListasBuscadas;
using Capa_de_Negocio;

namespace Capa_de_Presentacion
{
    public partial class frmCompras : Form
    {
        private USUARIO usuarioactual;
        public frmCompras(USUARIO _usuario = null)
        {
            usuarioactual = _usuario;
            InitializeComponent();
        }

        private void frmCompras_Load(object sender, EventArgs e)
        {
            cmbdocumento.Items.Add(new OpcionCombo() { valor = "Factura", texto = "Factura" });
            cmbdocumento.Items.Add(new OpcionCombo() { valor = "Boleta", texto = "Boleta" });
            cmbdocumento.DisplayMember = "texto";
            cmbdocumento.ValueMember = "valor";
            cmbdocumento.SelectedIndex = 0;

            txtfechacompra.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtidproducto.Text = "0";
            txtidproveedor.Text = "0";
        }

        private void btnbuscarproveedor_Click(object sender, EventArgs e)
        {
            using (var lista = new listaproveedores())
            {
                var resultado = lista.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    txtidproveedor.Text = lista.proveedorselecionado.IdProveedor.ToString();
                    txtdocumento.Text = lista.proveedorselecionado.Documento;
                    txtrazonsocial.Text = lista.proveedorselecionado.RazonSocial;
                }
                else
                {
                    txtdocumento.Select();
                }
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var lista = new listaproductos())
            {
                var resultado = lista.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    txtcodigoproducto.Text = lista.productoelegido.Codigo;
                    txtidproducto.Text = (lista.productoelegido.IdProducto).ToString();
                    txtproducto.Text = lista.productoelegido.Nombre;
                    txtpreciocompra.Text = lista.productoelegido.PrecioCompra.ToString();
                    txtprecioventa.Text = lista.productoelegido.PrecioVenta.ToString();
                }
                else
                {
                    txtdocumento.Select();
                }
            }
        }

        private void txtcodigoproducto_KeyDown(object sender, KeyEventArgs e)
        {
            PRODUCTO op = new CN_PRODUCTOS().Listar().Where(p => p.Codigo == txtcodigoproducto.Text && p.Estado == true).FirstOrDefault();

            if (op != null)
            {
                txtcodigoproducto.BackColor = Color.Honeydew;
                txtcodigoproducto.Text = op.Codigo.ToString();
                txtidproducto.Text = op.IdProducto.ToString();
                txtproducto.Text = op.Nombre;
                txtpreciocompra.Select();
            }
            else
            {
                txtcodigoproducto.BackColor = Color.MistyRose;
                txtidproducto.Text = "";
                txtproducto.Text = "";
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            decimal preciocompra = 0;
            decimal precioventa = 0;
            bool producto_existente = false;

            if (Convert.ToInt32(txtidproducto.Text) == 0)
            {
                MessageBox.Show("Seleccione un producto");
            }
            if (!decimal.TryParse(txtpreciocompra.Text, out preciocompra))
            {
                MessageBox.Show("Precio Compra - Formato incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (!decimal.TryParse(txtprecioventa.Text, out precioventa))
            {
                MessageBox.Show("Precio Venta - Formato incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtidproducto.Text)
                    producto_existente = true;
                break;
            }
            if (!producto_existente)
            {
                dataGridView1.Rows.Add(new object[]
                {
                    txtidproducto.Text,
                    txtcodigoproducto.Text,
                    txtproducto.Text,
                    preciocompra.ToString("0.00"),
                    precioventa.ToString("0.00"),
                    numericUpDown1.Value,
                   (preciocompra*numericUpDown1.Value).ToString("0.00")
                });
                calculartotal();
                limpiar();
                txtcodigoproducto.Select();
            }

        }
            private void calculartotal()
            {
            decimal total = 0;
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                decimal valor = decimal.Parse(fila.Cells["Subtotal"].Value.ToString());
                total += valor;
                txttotal.Text = total.ToString();
            }

            }   
        private void limpiar()
        {
            txtidproducto.Text = "0";
            txtcodigoproducto.Text = "";
            txtproducto.Text = "";
            txtpreciocompra.Text = "";
            txtprecioventa.Text = "";
            numericUpDown1.Value = 1;
        }

        
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            txtindicerow.Text = iRow.ToString();
            txtidproducto.Text = dataGridView1.Rows[iRow].Cells["IdProducto"].Value.ToString();
            txtcodigoproducto.Text = dataGridView1.Rows[iRow].Cells["Codigo"].Value.ToString();
            txtproducto.Text = dataGridView1.Rows[iRow].Cells["Producto"].Value.ToString();
            txtpreciocompra.Text= dataGridView1.Rows[iRow].Cells["PrecioCompra"].Value.ToString();
            txtprecioventa.Text = dataGridView1.Rows[iRow].Cells["PrecioVenta"].Value.ToString();
            numericUpDown1.Value= Convert.ToDecimal(dataGridView1.Rows[iRow].Cells["Cantidad"].Value.ToString());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                dataGridView1.Rows.RemoveAt(int.Parse(txtindicerow.Text));
                limpiar();
            }
            if (dataGridView1.Rows.Count ==1)
            {
                dataGridView1.Rows.RemoveAt(0);
                limpiar();
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtidproveedor.Text) == 0)
            {
                MessageBox.Show("Seleccione un Proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (dataGridView1.Rows.Count<1)
            {
                MessageBox.Show("Debe ingresar un Producto en la Compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            DataTable detalle_compra = new DataTable();
            detalle_compra.Columns.Add("IdProducto",typeof(int));
            detalle_compra.Columns.Add("PrecioCompra",typeof(decimal));
            detalle_compra.Columns.Add("PrecioVenta",typeof(decimal));
            detalle_compra.Columns.Add("Cantidad",typeof(int));
            detalle_compra.Columns.Add("SubTotal",typeof(decimal));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                detalle_compra.Rows.Add(
                    new object[]
                    {
                    Convert.ToInt32( row.Cells["IdProducto"].Value.ToString()),
                    row.Cells["PrecioCompra"].Value.ToString(),
                    row.Cells["PrecioVenta"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["Subtotal"].Value.ToString()
                    }
                    );
            }

            int idcorrelativo = new CN_COMPRA().ObtenerCorrelativo();

            string numerodocumento = string.Format("{0:00000}", idcorrelativo);

            COMPRA oCompra = new COMPRA()
            {
                Usuario = new USUARIO() { IdUsuario = usuarioactual.IdUsuario },
                Proveedor = new PROVEEDOR() { IdProveedor = Convert.ToInt32(txtidproveedor.Text) },
                TipoDocumento = ((OpcionCombo)cmbdocumento.SelectedItem).texto,
                NumeroDocumento = numerodocumento,
                MontoTotal=Convert.ToDecimal(txttotal.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_COMPRA().registrar(oCompra, detalle_compra, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Númreo de compra generada: \n"+numerodocumento+"\n\n " +
                    "Desea copiarlo al portapapeles?","Mensaje",MessageBoxButtons.YesNo,MessageBoxIcon.Information);

                if (result==DialogResult.Yes)
                {
                    Clipboard.SetText(numerodocumento);
                }
                txtidproducto.Text = "0";
                txtdocumento.Text = "" ;
                txtrazonsocial.Text = "";
                dataGridView1.Rows.Clear();
                calculartotal();

            }
        }

    }
}
