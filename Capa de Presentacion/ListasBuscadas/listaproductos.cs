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
using Capa_de_Negocio;


namespace Capa_de_Presentacion.ListasBuscadas
{
    public partial class listaproductos : Form
    {
        public PRODUCTO productoelegido;
        public listaproductos()
        {
            InitializeComponent();
        }

        private void listaproductos_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                if (columna.Visible == true)
                {
                    cmbbusqueda.Items.Add(new OpcionCombo() { valor = columna.Name, texto = columna.HeaderText });
                }
            }
            cmbbusqueda.DisplayMember = "texto";
            cmbbusqueda.ValueMember = "valor";
            cmbbusqueda.SelectedIndex = 0;

            List<PRODUCTO> listaProductos = new CN_PRODUCTOS().Listar();
            foreach (PRODUCTO PRODUCTO in listaProductos)
            {
                dataGridView1.Rows.Add(new object[] {PRODUCTO.IdProducto,PRODUCTO.Codigo,PRODUCTO.Nombre,PRODUCTO.Categoria.Descripcion,PRODUCTO.Stock,
                    PRODUCTO.PrecioCompra,PRODUCTO.PrecioVenta
                                                });
            }
        }

        private void buscador_Click(object sender, EventArgs e)
        {
            string columna = ((OpcionCombo)cmbbusqueda.SelectedItem).valor.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[columna].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int icolum = e.ColumnIndex;

            if (iRow>=0 && icolum>0)
            {
                productoelegido = new PRODUCTO()
                {
                    IdProducto = Convert.ToInt32(dataGridView1.Rows[iRow].Cells["IdProducto"].Value),
                    Codigo = dataGridView1.Rows[iRow].Cells["Codigo"].Value.ToString(),
                    Nombre = dataGridView1.Rows[iRow].Cells["Nombre"].Value.ToString(),
                    Categoria = new CATEGORIA()
                    {
                        Descripcion = dataGridView1.Rows[iRow].Cells["Categoria"].Value.ToString()

                    },
                    Stock = Convert.ToInt32(dataGridView1.Rows[iRow].Cells["Stock"].Value),
                    PrecioCompra = Convert.ToDecimal(dataGridView1.Rows[iRow].Cells["PrecioCompra"].Value),
                    PrecioVenta = Convert.ToDecimal(dataGridView1.Rows[iRow].Cells["PrecioVenta"].Value)

                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
