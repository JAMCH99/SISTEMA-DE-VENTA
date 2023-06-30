using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Capa_de_Presentacion.Utilidades;
using Capa_Entidad;
using Capa_de_Negocio;

namespace Capa_de_Presentacion
{
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
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

            List<ROL> lista = new CN_ROL().lista();

            

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

            List<CATEGORIA> lista_categoria = new CN_CATEGORIA().Listar();
            foreach (CATEGORIA CATEGORIA in lista_categoria)
            {
                dataGridView1.Rows.Add(new object[] {CATEGORIA.IdCategoria,                                              
                                                CATEGORIA.Descripcion,
                                                CATEGORIA.Estado==true ? 1:0,
                                                CATEGORIA.Estado== true? "Activo":"Inactivo"
                                                });
            }
        }
        private void limpiar()
        {
            txtindicerow.Text = "-1"; txtid.Text = "0"; txtdescripcion.Text = "";
            
            cmbestado.SelectedIndex = 0;
        }
     
        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            CATEGORIA objCATEGORIA = new CATEGORIA()
            {
                IdCategoria = Convert.ToInt32(txtid.Text),
                Descripcion = txtdescripcion.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).valor) == 1 ? true : false

            };
            if (objCATEGORIA.IdCategoria == 0)
            {
                int idgeneradoCATEGORIA = new CN_CATEGORIA().Registrar(objCATEGORIA, out mensaje);

                if (idgeneradoCATEGORIA != 0)
                {
                    dataGridView1.Rows.Add(new object[] { txtid.Text,txtdescripcion.Text,
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
                bool consulta = new CN_CATEGORIA().Editar(objCATEGORIA, out mensaje);
                if (consulta)
                {
                    DataGridViewRow row = dataGridView1.Rows[int.Parse(txtindicerow.Text)];
                    row.Cells["IdCategoria"].Value = txtid.Text;
                    row.Cells["Descripcion"].Value = txtdescripcion.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbestado.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).texto.ToString();
                    limpiar();
                }
                else { MessageBox.Show(mensaje); }
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            txtindicerow.Text = indice.ToString();
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtdescripcion.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            foreach (OpcionCombo oc in cmbestado.Items)
            {
                if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value))
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
                if (MessageBox.Show("Esta seguro de eliminar esta Categoria ?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CATEGORIA obj = new CATEGORIA()
                    {
                        IdCategoria = Convert.ToInt32(txtid.Text)
                    };
                    bool resultado = new CN_CATEGORIA().Eliminar(obj, out mensaje);

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

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            limpiar(); 
        }
    }
}
