using Capa_de_Presentacion.ListasBuscadas;
using Capa_de_Presentacion.Utilidades;
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
using Capa_de_Negocio;

namespace Capa_de_Presentacion
{
    public partial class frmVentas : Form
    {
        private USUARIO _usuarioactual = null;

        public frmVentas(USUARIO usuarioactual)
        {
            InitializeComponent();
            _usuarioactual = usuarioactual;
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            cmbdocumento.Items.Add(new OpcionCombo() { valor = "Factura", texto = "Factura" });
            cmbdocumento.Items.Add(new OpcionCombo() { valor = "Boleta", texto = "Boleta" });
            cmbdocumento.DisplayMember = "texto";
            cmbdocumento.ValueMember = "valor";
            cmbdocumento.SelectedIndex = 0;

            txtfechacompra.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtidusuario.Text = _usuarioactual.IdUsuario.ToString();
            txtidproducto.Text = "0";
            txttotal.Text = "0";
            txtpago.Text = "";
            txtcambio.Text = "";
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            using (var lista = new listaproductos())
            {
                var resultado = lista.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    txtidproducto.Text = lista.productoelegido.IdProducto.ToString();
                    txtcodigoproducto.Text = lista.productoelegido.Codigo;
                    txtproducto.Text = lista.productoelegido.Nombre.ToString();
                    txtstock.Text = lista.productoelegido.Stock.ToString();
                    txtprecio.Text = lista.productoelegido.PrecioVenta.ToString();
                    numericUpDown1.Select();

                    productoactual = new PRODUCTO() { IdProducto=lista.productoelegido.IdProducto, Stock = lista.productoelegido.Stock, Codigo=lista.productoelegido.Codigo };

            }
                else
                {
                    txtdocumento.Select();
                }
            }
        }

        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
            using (var lista = new listaclientes())
            {
                var resultado = lista.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    txtdocumento.Text = lista.clienteselecionado.Documento;
                    txtcliente.Text = lista.clienteselecionado.NombreCompleto;
                }
                else
                {
                    txtdocumento.Select();
                }
            }
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal precio = 0;
                bool producto_existente = false;

                if (Convert.ToInt32(txtidproducto.Text) == 0)
                {
                    throw new Exception ( "Seleccione un producto"); 
                }

                if (!decimal.TryParse(txtprecio.Text, out precio))
                {
                    throw new Exception("Precio Venta - Formato incorrecto");
                }
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    if (fila.Cells["IdProducto"].Value.ToString() == txtidproducto.Text)
                        producto_existente = true;
                    break;
                }

                if (!producto_existente)
                {
                    
                    if (txtstock.Text == "0") { MessageBox.Show("No se puede realizar la venta \n \n No queda productos en el stock \n \n Realize una compra del producto !"); return; }

                    bool stock = new CN_VENTA().Restar_Stock(Convert.ToInt32(txtidproducto.Text), Convert.ToInt32(numericUpDown1.Value));


                    if (stock)
                    {
                    dataGridView1.Rows.Add(new object[]
                    {
                    txtidproducto.Text,
                    txtproducto.Text,
                    precio.ToString("0.00"),
                    numericUpDown1.Value,
                   (precio*numericUpDown1.Value).ToString("0.00")
                    });
                    calculartotal();
                    limpiar();
                    txtcodigoproducto.Select();

                    }
                }
                else
                {
                    var result = MessageBox.Show("El producto ya se encuentra en el carrito... \n \n Desea añadir otra vez el mismo producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (DialogResult.Yes == result)
                    {
                        producto_existente = false;
                        dataGridView1.Rows.Add(new object[]
                    {
                    txtidproducto.Text,
                    txtproducto.Text,
                    precio.ToString("0.00"),
                    numericUpDown1.Value,
                   (precio*numericUpDown1.Value).ToString("0.00")
                    });
                        calculartotal();
                        limpiar();
                        txtcodigoproducto.Select();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
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
            txtprecio.Text = "";
            txtstock.Text = "";
            numericUpDown1.Value = 1;
        }

        int cantidad = 0;
        private PRODUCTO productoactual = null;
        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                dataGridView1.Rows.RemoveAt(int.Parse(txtindicerow.Text));
                limpiar();
            }
            if (dataGridView1.Rows.Count == 1)
            {
                dataGridView1.Rows.RemoveAt(0);
                limpiar();

                int idproducto = productoactual.IdProducto;

                bool stock = new CN_VENTA().Sumar_Stock(Convert.ToInt32(idproducto), cantidad);
            }
            txtpago.Text = "";
            txtcambio.Text = "";
            txttotal.Text = "";
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;

            txtindicerow.Text = indice.ToString();
            txtproducto.Text = dataGridView1.Rows[indice].Cells["Producto"].Value.ToString();
            txtprecio.Text = dataGridView1.Rows[indice].Cells["Precio"].Value.ToString();
            txtstock.Text = productoactual.Stock.ToString();
            txtcodigoproducto.Text = productoactual.Codigo.ToString();
            numericUpDown1.Value = Convert.ToInt32(dataGridView1.Rows[indice].Cells["Cantidad"].Value.ToString());

            cantidad=Convert.ToInt32(dataGridView1.Rows[indice].Cells["Cantidad"].Value.ToString());

           
        }
        private void calcular_cambio()
        {
            if (txttotal.Text.Trim() == "") { MessageBox.Show("No existen Productos para la venta", "Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (txtpago.Text.Length ==0 ) { MessageBox.Show("Ingrese un monto para pagar", "Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            decimal cambio = Convert.ToDecimal(txtpago.Text)- Convert.ToDecimal(txttotal.Text);
            if(Convert.ToDecimal(txtpago.Text)< Convert.ToDecimal(txttotal.Text))
                { MessageBox.Show("El monto ingresado es menor al total a pagar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                txtcambio.Text = cambio.ToString();
        }
        private void txtpago_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData== Keys.Enter)
            {
                calcular_cambio();
            }
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            if (txtcliente.Text == "") { MessageBox.Show("Debe selecionar un cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (txtdocumento.Text == "") { MessageBox.Show("Debe selecionar un cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dataGridView1.Rows.Count < 1) { MessageBox.Show("Selecione un Producto para realizar una venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            DataTable tabla = new DataTable();
            tabla.Columns.Add("IdProducto", typeof(int));
            tabla.Columns.Add("PrecioVenta", typeof(decimal));
            tabla.Columns.Add("Cantidad", typeof(int));
            tabla.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                tabla.Rows.Add(new object[]{

                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["Subtotal"].Value.ToString()
                }) ;
            }

            int correlativo = new CN_VENTA().ObtenerCorrelativo();
            string numerodocumento = string.Format("{0:00000}",correlativo);
            calcular_cambio();

            VENTA oventa = new VENTA()
            {
                Usuario = new USUARIO() { IdUsuario = _usuarioactual.IdUsuario },
                TipoDocumento = ((OpcionCombo)cmbdocumento.SelectedItem).texto,
                NumeroDocumento = numerodocumento,
                DocumentoCliente = txtdocumento.Text,
                NombreCliente = txtcliente.Text,
                MontoPago = Convert.ToDecimal(txtpago.Text),
                MontoCambio = Convert.ToDecimal(txtcambio.Text),
                MontoTotal= Convert.ToDecimal(txttotal.Text)
            };
           
            string mensaje = string.Empty;

            bool registro = new CN_VENTA().registrar(oventa,tabla,out mensaje);

            if (!registro) { MessageBox.Show(mensaje); }
            if (registro)
            {
                var result = MessageBox.Show("Númreo de venta generada: \n" + numerodocumento + "\n\n " +
                    "Desea copiarlo al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numerodocumento);
                }
                txtidproducto.Text = "0";
                txtdocumento.Text = "";
                txtcliente.Text = "";
                txtpago.Text = "";
                txtcambio.Text = "";
                dataGridView1.Rows.Clear();
                calculartotal();

            }
        }

    }
}
