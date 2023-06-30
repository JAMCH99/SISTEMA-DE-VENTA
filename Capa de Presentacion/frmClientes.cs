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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
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

            List<CLIENTE> listaCliente = new CN_CLIENTE().Listar();
            foreach (CLIENTE cliente in listaCliente)
            {
                dataGridView1.Rows.Add(new object[] {cliente.IdCliente,cliente.Documento,cliente.NombreCompleto,cliente.Correo,cliente.Telefono,
                                                cliente.Estado==true ? 1:0,
                                                cliente.Estado== true? "Activo":"Inactivo"
                                                });
            }
        }
        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            CLIENTE objcliente = new CLIENTE()
            {
                IdCliente = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombre.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).valor) == 1 ? true : false

            };
            if (objcliente.IdCliente == 0)
            {


                int idgeneradocliente = new CN_CLIENTE().Registrar(objcliente, out mensaje);

                if (idgeneradocliente != 0)
                {
                    dataGridView1.Rows.Add(new object[] { txtid.Text,txtdocumento.Text,txtnombre.Text,txtcorreo.Text,txttelefono.Text,                                            
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
                bool consulta = new CN_CLIENTE().Editar(objcliente, out mensaje);
                if (consulta)
                {
                    DataGridViewRow row = dataGridView1.Rows[int.Parse(txtindicerow.Text)];
                    row.Cells["IdCliente"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtnombre.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Telefono"].Value = txttelefono.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbestado.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).texto.ToString();
                }
                else { MessageBox.Show(mensaje); }
            }
        }

        private void limpiar()
        {
            txtindicerow.Text = "-1"; txtid.Text = "0"; txtdocumento.Text = ""; txtnombre.Text = "";
            txtcorreo.Text = ""; txttelefono.Text = ""; 
            cmbestado.SelectedIndex = 0;
        }
        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtid.Text) != 0)
            {
                string mensaje = string.Empty;
                if (MessageBox.Show("Esta seguro de eliminar este CLIENTE?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CLIENTE objCliente = new CLIENTE()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text)
                    };
                    bool resultado = new CN_CLIENTE().Eliminar(objCliente, out mensaje);

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            txtindicerow.Text = indice.ToString();
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtdocumento.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtnombre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtcorreo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txttelefono.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
 

           
            foreach (OpcionCombo oc in cmbestado.Items)
            {
                if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value))
                {
                    cmbestado.SelectedIndex = cmbestado.Items.IndexOf(oc);
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
    }
}
