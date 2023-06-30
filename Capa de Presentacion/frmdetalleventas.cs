using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_de_Presentacion.ListasBuscadas;
using Capa_Entidad;
using Capa_de_Negocio;
using Capa_de_Presentacion.Utilidades;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Capa_de_Presentacion
{
    public partial class frmdetalleventas : Form
    {
        public frmdetalleventas()
        {
            InitializeComponent();
        }

        private void btndownload_Click(object sender, EventArgs e)
        {
            if (txtnumerobuscado.Text == "") { MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);return; }

            string texto_HTML = Properties.Resources.PlantillaCompra.ToString();
            NEGOCIO onegocio = new CN_NEGOCIO().Obtener_Datos();

            // modificamos la plantilla creada en html 

            texto_HTML = texto_HTML.Replace("@nombrenegocio", onegocio.Nombre.ToUpper());
            texto_HTML = texto_HTML.Replace("@direcnegocio", onegocio.Direccion);
            texto_HTML = texto_HTML.Replace("@docnegocio", onegocio.Ruc);

            texto_HTML = texto_HTML.Replace("@tipodocumento", txtTipodocumento.Text.ToUpper());
            texto_HTML = texto_HTML.Replace("@numerodocumento", txtnumerobuscado.Text);

            texto_HTML = texto_HTML.Replace("@docproveedor", txtdocumentocliente.Text);
            texto_HTML = texto_HTML.Replace("@nombreproveedor", txtcliente.Text);
            texto_HTML = texto_HTML.Replace("@fecharegistro", txtfechaventa.Text);
            texto_HTML = texto_HTML.Replace("@usuarioregistro", txtusuario.Text);

            string filas = string.Empty;

            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + rows.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + rows.Cells["PrecioVenta"].Value.ToString() + "</td>";
                filas += "<td>" + rows.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + rows.Cells["Subtotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            texto_HTML = texto_HTML.Replace("@filas", filas);
            texto_HTML = texto_HTML.Replace("@montototal", txttotal.Text);


            // Creación dl PDF

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Venta_{0}.pdf", txtnumerobuscado.Text);
            saveFile.Filter = "Pdf Files | *.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();

                    bool obtenido = true;
                    byte[] image = new CN_NEGOCIO().ObtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        //convierte el array de imagenes en una imagen

                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(image);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;  //la imagen sobre el texto 
                        img.SetAbsolutePosition(pdfdoc.Left, pdfdoc.GetTop(50));
                        pdfdoc.Add(img);
                    }

                    // añadiremos el html sobre el documento pdf creado

                    using (StringReader sr = new StringReader(texto_HTML))
                    {
                        //simulara que todo sera un html

                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfdoc, sr);
                    }
                    pdfdoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
        }

        private void buscador_Click_1(object sender, EventArgs e)
        {
            VENTA oventa = new CN_VENTA().Obtener_Venta(((OpcionCombo)cmbnumerodocumento.SelectedItem).texto);
            if (oventa.IdVenta != 0)
            {
                txtfechaventa.Text = oventa.FechaRegistro;
                txtusuario.Text = oventa.Usuario.NombreCompleto;
                txtTipodocumento.Text = oventa.TipoDocumento;
                txtdocumentocliente.Text = oventa.DocumentoCliente;
                txtcliente.Text = oventa.NombreCliente;
                txtnumerobuscado.Text = ((OpcionCombo)cmbnumerodocumento.SelectedItem).texto;
                dataGridView1.Rows.Clear();
                foreach (DETALLE_VENTA dv in oventa.list_Detalle_Venta)
                {
                    dataGridView1.Rows.Add(
                        new object[] {

                        dv.Producto.Nombre, dv.PrecioVenta,dv.Cantidad,dv.Subtotal
                        }
                        );
                }
                txtpago.Text = oventa.MontoPago.ToString("0.00");
                txtcambio.Text = oventa.MontoCambio.ToString("0.00");
                txttotal.Text = oventa.MontoTotal.ToString("0.00");
                txtnumerobuscado.Text = ((OpcionCombo)cmbnumerodocumento.SelectedItem).texto;
            }
            else{
                MessageBox.Show("no se encuentra el número buscado");
            }
        }

        private void frmdetalleventas_Load_1(object sender, EventArgs e)
        {
            List<VENTA> lista = new CN_VENTA().Obtener_numero();
            foreach (VENTA item in lista)
            {
                cmbnumerodocumento.Items.Add(new OpcionCombo() { valor = item.IdVenta, texto = item.NumeroDocumento });
                cmbnumerodocumento.DisplayMember = "texto";
                cmbnumerodocumento.ValueMember = "valor";
                cmbnumerodocumento.SelectedIndex = 0;
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtfechaventa.Text = "";
            txtusuario.Text = "";
            txtTipodocumento.Text = "";
            txtcliente.Text = "";
            txtdocumentocliente.Text = "";
            txtpago.Text = "";
            txtcambio.Text = "";
            txttotal.Text = "";
            dataGridView1.Rows.Clear();
        }
    }

    
}
