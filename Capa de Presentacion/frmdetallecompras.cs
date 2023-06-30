using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_de_Negocio;
using Capa_de_Presentacion.Utilidades;
using Capa_Entidad;


// para poder crear el pdf

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Capa_de_Presentacion
{
    public partial class frmdetallecompras : Form
    {
        public frmdetallecompras()
        {
            InitializeComponent();
        }

        private void frmdetallecompras_Load(object sender, EventArgs e)
        {
            List<COMPRA> lista = new CN_COMPRA().listanuemro_compra();

            foreach (COMPRA item in lista)
            {
                cmbnumerodocumento.Items.Add(new OpcionCombo() { valor =item.IdCompra , texto =item.NumeroDocumento  });
                cmbnumerodocumento.DisplayMember = "texto";
                cmbnumerodocumento.ValueMember = "valor";
                cmbnumerodocumento.SelectedIndex = 0;
            }
        }

        private void buscador_Click(object sender, EventArgs e)
        {
            
            COMPRA ocompra = new CN_COMPRA().ObtenerCompra(((OpcionCombo)cmbnumerodocumento.SelectedItem).texto);
            if (ocompra.IdCompra != 0)
            {
                txtfechacompra.Text = ocompra.FechaRegistro;
                txtusuario.Text = ocompra.Usuario.NombreCompleto;
                txtTipodocumento.Text = ocompra.TipoDocumento;
                txtnumeroproveedor.Text = ocompra.Proveedor.Documento;
                txtrazonsocial.Text = ocompra.Proveedor.RazonSocial;
                txtnumerobuscado.Text = ((OpcionCombo)cmbnumerodocumento.SelectedItem).texto;
                dataGridView1.Rows.Clear();
                foreach (DETALLE_COMPRA dc in ocompra.lista_Detalle_Compra)
                {
                    dataGridView1.Rows.Add(
                        new object[] {

                        dc.Producto.Nombre, dc.PrecioCompra,dc.Cantidad,dc.Montototal
                        }
                        );
                }
                txtmontototal.Text = ocompra.MontoTotal.ToString("0.00");
            }
            
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtfechacompra.Text = "";
            txtusuario.Text = "";
            txtTipodocumento.Text = "";
            txtnumeroproveedor.Text = "";
            txtrazonsocial.Text = "";
            txtmontototal.Text = "";
            dataGridView1.Rows.Clear();
        }
        private void btndownload_Click(object sender, EventArgs e)
        {
            if (txtnumerobuscado.Text == "") { MessageBox.Show("No se encontraron reslutados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            string texto_HTML = Properties.Resources.PlantillaCompra.ToString();
            NEGOCIO onegocio = new CN_NEGOCIO().Obtener_Datos();

            // modificamos la plantilla creada en html 

            texto_HTML = texto_HTML.Replace("@nombrenegocio", onegocio.Nombre.ToUpper());
            texto_HTML = texto_HTML.Replace("@direcnegocio", onegocio.Direccion);
            texto_HTML = texto_HTML.Replace("@docnegocio",onegocio.Ruc);

            texto_HTML = texto_HTML.Replace("@tipodocumento",txtTipodocumento.Text.ToUpper());
            texto_HTML = texto_HTML.Replace("@numerodocumento",txtnumerobuscado.Text );

            texto_HTML = texto_HTML.Replace("@docproveedor", txtnumeroproveedor.Text);
            texto_HTML = texto_HTML.Replace("@nombreproveedor", txtrazonsocial.Text);
            texto_HTML = texto_HTML.Replace("@fecharegistro", txtfechacompra.Text);
            texto_HTML = texto_HTML.Replace("@usuarioregistro",txtusuario.Text);

            string filas = string.Empty;

            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + rows.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + rows.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + rows.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + rows.Cells["Subtotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            texto_HTML = texto_HTML.Replace("@filas",filas);
            texto_HTML = texto_HTML.Replace("@montototal",txtmontototal.Text);

            
            // Creación dl PDF
            
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = string.Format("Compra_{0}.pdf", txtnumerobuscado.Text);
            saveFile.Filter = "Pdf Files | *.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4,25,25,25,25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfdoc,stream);
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

    }
}
