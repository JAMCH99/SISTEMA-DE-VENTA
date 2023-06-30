using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Capa_de_Negocio;
using Capa_Entidad;
using Capa_de_Presentacion.Utilidades;

// para guardar en Excel
using ClosedXML.Excel;

namespace Capa_de_Presentacion
{
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            txtid.Text = "0";
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cmbestado.Items.Add(new OpcionCombo() { valor = 1, texto = "Activo" });
            cmbestado.Items.Add(new OpcionCombo() { valor = 0, texto = "Inactivo" });

            // para que muestre los valores

            cmbestado.DisplayMember = "texto";
            cmbestado.ValueMember = "valor";
            cmbestado.SelectedIndex = 0;

            List<CATEGORIA> lista = new CN_CATEGORIA().Listar();

            foreach (CATEGORIA item in lista)
            {
                cmbcategoria.Items.Add(new OpcionCombo() { valor = item.IdCategoria, texto = item.Descripcion });
            }

            cmbcategoria.DisplayMember = "texto";
            cmbcategoria.ValueMember = "valor";
            cmbcategoria.SelectedIndex = 0;

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

            List<PRODUCTO> listaPRODUCTO = new CN_PRODUCTOS().Listar();
            foreach (PRODUCTO producto in listaPRODUCTO)
            {
                dataGridView1.Rows.Add(new object[] {producto.IdProducto,producto.Codigo,producto.Nombre,producto.Descripcion,
                                                producto.Categoria.IdCategoria,
                                                producto.Categoria.Descripcion,
                                                producto.Stock,
                                                producto.PrecioCompra,
                                                producto.PrecioVenta,
                                                producto.Estado==true ? 1:0,
                                                producto.Estado== true? "Activo":"Inactivo"
                                                });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            PRODUCTO objproducto = new PRODUCTO()
            {
                IdProducto = Convert.ToInt32(txtid.Text),
                Codigo = txtcodigo.Text,
                Nombre = txtnombre.Text,
                Descripcion = txtdescripcion.Text,
                Categoria = new CATEGORIA() { IdCategoria = Convert.ToInt32(((OpcionCombo)cmbcategoria.SelectedItem).valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).valor) == 1 ? true : false

            };
            if (objproducto.IdProducto == 0)
            {


                int idgeneradoproducto = new CN_PRODUCTOS().Registrar(objproducto, out mensaje);

                if (idgeneradoproducto != 0)
                {
                    dataGridView1.Rows.Add(new object[] { txtid.Text,txtcodigo.Text,txtnombre.Text,txtdescripcion.Text,
                                                ((OpcionCombo)cmbcategoria.SelectedItem).valor.ToString(),
                                                ((OpcionCombo)cmbcategoria.SelectedItem).texto.ToString(),
                                                ((OpcionCombo)cmbestado.SelectedItem).valor.ToString(),
                                                ((OpcionCombo)cmbestado.SelectedItem).texto.ToString(),
                                                });
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
                limpiar();
            }
            else
            {
                bool consulta = new CN_PRODUCTOS().Editar(objproducto, out mensaje);
                if (consulta)
                {
                    DataGridViewRow row = dataGridView1.Rows[int.Parse(txtindicerow.Text)];
                    row.Cells["IdProducto"].Value = txtid.Text;
                    row.Cells["Codigo"].Value = txtcodigo.Text;
                    row.Cells["Nombre"].Value = txtnombre.Text;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["IdCategoria"].Value = ((OpcionCombo)cmbcategoria.SelectedItem).valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)cmbcategoria.SelectedItem).texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbestado.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).texto.ToString();
                }
                else { MessageBox.Show(mensaje); }
            }
        }
        private void limpiar()
        {
            txtindicerow.Text = "-1"; txtid.Text = "0"; txtcodigo.Text = ""; txtnombre.Text = "";
            txtdescripcion.Text = "";
            cmbcategoria.SelectedIndex = 0;
            cmbestado.SelectedIndex = 0;
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            txtindicerow.Text = indice.ToString();
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtcodigo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtnombre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtdescripcion.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            

            foreach (OpcionCombo oc in cmbcategoria.Items)
            {
                if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridView1.CurrentRow.Cells[4].Value))
                {
                    cmbcategoria.SelectedIndex = cmbcategoria.Items.IndexOf(oc);
                }
            }
            foreach (OpcionCombo oc in cmbestado.Items)
            {
                if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridView1.CurrentRow.Cells[6].Value))
                {
                    cmbestado.SelectedIndex = cmbestado.Items.IndexOf(oc);
                }
            }
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtid.Text) != 0)
            {
                string mensaje = string.Empty;
                if (MessageBox.Show("Esta seguro de eliminar este Producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PRODUCTO objproducto = new PRODUCTO()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text)
                    };
                    bool resultado = new CN_PRODUCTOS().Eliminar(objproducto, out mensaje);

                    if (resultado)
                    {
                        dataGridView1.Rows.RemoveAt(int.Parse(txtindicerow.Text));
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
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

        private void descargar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach(DataGridViewColumn columna in dataGridView1.Columns)
                {
                    if (columna.Visible)
                    {
                        dt.Columns.Add(columna.HeaderText,typeof (string));
                    }
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Visible)
                    {
                        dt.Rows.Add(new object[] {
                        row.Cells[1].Value.ToString(),
                        row.Cells[2].Value.ToString(),
                        row.Cells[3].Value.ToString(),
                        row.Cells[5].Value.ToString(),
                        row.Cells[6].Value.ToString(),
                        row.Cells[7].Value.ToString(),
                        row.Cells[8].Value.ToString(),
                        row.Cells[10].Value.ToString(),
                        });
                    }
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteProducto_{0}.xlsx",DateTime.Now.ToString("dd-MM-" +
                    "yyyy"));
                saveFile.Filter = "Excel Files | *.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();

                        var hoja = wb.Worksheets.Add(dt, "INFORME");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);

                        MessageBox.Show("Reporte Generado","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Reporte no Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }

            

        }
    }
}
