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

using System.IO;

namespace Capa_de_Presentacion
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        private void frmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;

            byte[] imagen = new CN_NEGOCIO().ObtenerLogo(out obtenido);
            if (obtenido)
                pictureBox1.Image = bytetoimage(imagen);

            NEGOCIO datos = new CN_NEGOCIO().Obtener_Datos();

            txtnombre.Text = datos.Nombre;
            txtruc.Text = datos.Ruc;
            txtdireccion.Text = datos.Direccion;
        }

        // convierte los bytes en imagen
        public Image bytetoimage(byte[] imagen)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imagen, 0, imagen.Length);
            Image img = new Bitmap(ms);
            return img;
        }

        private void subirImg_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.FileName = "Files|* .jpg;*.jpeg;*.png*";

            if (dialog.ShowDialog()==DialogResult.OK)
            {
                byte[] img = File.ReadAllBytes(dialog.FileName);
                bool actualizar = new CN_NEGOCIO().ActualizarLogo(img,out mensaje);

                if (actualizar)
                    pictureBox1.Image = bytetoimage(img);
                else
                    MessageBox.Show(mensaje);
            }
        }

        private void guardarcambios_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            NEGOCIO datos = new NEGOCIO()
            {
                Nombre = txtnombre.Text,
                Ruc = txtruc.Text,
                Direccion = txtdireccion.Text
            };

            bool resulatdo = new CN_NEGOCIO().GuardarDatos(datos,out mensaje);

            if (resulatdo)
                MessageBox.Show("Datos guardados correctamente","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("No se puedo guardar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);



        }
    }
}
