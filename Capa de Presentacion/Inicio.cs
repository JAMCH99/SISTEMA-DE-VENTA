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
using FontAwesome.Sharp;

namespace Capa_de_Presentacion
{
    public partial class Inicio : Form
    {
        private static USUARIO usuarioactual;
        private static IconMenuItem MenuActivo=null;
        private static Form FormularioActivo = null;
        public Inicio(USUARIO objusuario =null)
        {
            if (objusuario==null)
            {
                usuarioactual = new USUARIO() { NombreCompleto = "usuario predefenido", IdUsuario = 1 };
            }
            else
            {
            usuarioactual = objusuario;
            }
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            usuario.Text = usuarioactual.NombreCompleto;

            List<PERMISO> Lista_Permiso = new CN_PERMISO().Lista_Permiso(usuarioactual.IdUsuario);

            foreach (IconMenuItem iconmenu in menu.Items)
            {
                bool encontrado = Lista_Permiso.Any(m => m.NombreMenu == iconmenu.Name);
                    if (encontrado==false)
                    {
                    iconmenu.Visible = false;
                    }
            }

        }

        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            ACERCADE mostrar = new ACERCADE();
            mostrar.ShowDialog();
        }

        private void AbrirFormulario( IconMenuItem Menu, Form formulario) 
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            Menu.BackColor = Color.Silver;
            MenuActivo = Menu;
            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;//no haya bordes
            formulario.Dock = DockStyle.Fill; // para que se acople al contenedor donde se colocará
            contenedor.Controls.Add(formulario);
            formulario.BackColor = Color.GreenYellow;
            formulario.Show();
        }
        private void menuusuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuusuarios,new formUsuarios());
        }

        private void submenucategorias_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmCategoria());
        }

        private void submenuproductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmProductos());
        }

        

        private void menuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuclientes, new frmClientes());
        }



        private void menuproveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuproveedores, new frmProveedores());
        }

        private void submenuregistrarcompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCompras(usuarioactual));
        }

        private void submenuvercompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmdetallecompras());
        }

        private void submenuregistrarventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmVentas(usuarioactual));
        }

        private void submenudetalleventas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmdetalleventas());
        }
        private void submenunegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menumantenedor, new frmNegocio());
        }
        private void reporteComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new R_COMPRAS());
        }

        private void reporteVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new R_VENTAS());
        }

        private void logout_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("        ¿Desea salir? ", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
