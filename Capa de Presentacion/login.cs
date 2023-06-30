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

namespace Capa_de_Presentacion
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btningresar_Click(object sender, EventArgs e)
        {
            List<USUARIO> TEST = new CN_USUARIO().Listar();

            //la siguiente linea nos devuelve los valores de lo que se ingresa en los textbox
            USUARIO usuario = new CN_USUARIO().Listar().Where(a=>a.Documento==textBox1.Text && a.Clave==textBox2.Text).FirstOrDefault();
            Inicio form = new Inicio(usuario);
            if (usuario!=null)
            {
            form.Show();
            this.Hide();
            form.FormClosing += closing;
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("No se encuentra el Usuario", "Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void closing(object sender,FormClosingEventArgs e)
        {
           
            this.Show();
        }

        
    }
}
