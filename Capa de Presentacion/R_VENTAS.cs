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
using Capa_de_Negocio;
using Capa_Entidad;
using ClosedXML.Excel;

namespace Capa_de_Presentacion
{
    public partial class R_VENTAS : Form
    {
        public R_VENTAS()
        {
            InitializeComponent();
        }
        private void R_VENTAS_Load(object sender, EventArgs e)
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

            
        }

        private void btnbuscar_Click(object sender, EventArgs e)
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

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true;
            }

        }


        private void buscador_Click(object sender, EventArgs e)
        {
            List<REPORTE_VENTAS> lista = new CN_REPORTES().listarVentas(dateTimePicker1.Value.ToString(), dateTimePicker2.Value.ToString());

            foreach (REPORTE_VENTAS item in lista)
            {
                dataGridView1.Rows.Add(new object[] {
                item.FechaRegistro,item.TipoDocumneto,item.NumerDocumento,item.MontoTotal,item.UsuarioRegistro,item.DocumentoCliente,item.NombreCliente,
                item.CodigoProducto,item.NombreProducto,item.Categoria,item.PrecioVenta,item.Cantidad,item.Subtotal});
            }
        }

        private void btndowload_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn columna in dataGridView1.Columns)
                {
                    if (columna.Visible)
                    {
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                    }
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Visible)
                    {
                        dt.Rows.Add(new object[] {
                        row.Cells[0].Value.ToString(),
                        row.Cells[1].Value.ToString(),
                        row.Cells[2].Value.ToString(),
                        row.Cells[3].Value.ToString(),
                        row.Cells[4].Value.ToString(),
                        row.Cells[5].Value.ToString(),
                        row.Cells[6].Value.ToString(),
                        row.Cells[7].Value.ToString(),
                        row.Cells[8].Value.ToString(),
                        row.Cells[9].Value.ToString(),
                        row.Cells[10].Value.ToString(),
                        row.Cells[11].Value.ToString(),
                        row.Cells[12].Value.ToString(),
                        });
                    }
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("dd-MM-" +
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

                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
