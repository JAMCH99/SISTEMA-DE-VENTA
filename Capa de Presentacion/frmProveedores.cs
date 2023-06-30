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

namespace Capa_de_Presentacion
{
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }


        private void frmProveedores_Load(object sender, EventArgs e)
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

            List<PROVEEDOR> listaProveedores = new CN_PROVEEDOR().Listar();
            foreach (PROVEEDOR PROVEEDOR in listaProveedores)
            {
                dataGridView1.Rows.Add(new object[] {PROVEEDOR.IdProveedor,PROVEEDOR.Documento,PROVEEDOR.RazonSocial,PROVEEDOR.Correo,PROVEEDOR.Telefono,
                                                PROVEEDOR.Estado==true ? 1:0,
                                                PROVEEDOR.Estado== true? "Activo":"Inactivo"
                                                });
            }
        }
        private void limpiar()
        {
            txtindicerow.Text = "-1"; txtid.Text = "0"; txtdocumento.Text = ""; txtrazonsocial.Text = "";
            txtcorreo.Text = ""; txttelefono.Text = "";
            cmbestado.SelectedIndex = 0;
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            PROVEEDOR objPROVEEDOR = new PROVEEDOR()
            {
                IdProveedor = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                RazonSocial = txtrazonsocial.Text,
                Correo = txtcorreo.Text,
                Telefono = txttelefono.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).valor) == 1 ? true : false

            };
            if (objPROVEEDOR.IdProveedor == 0)
            {


                int idgeneradoPROVEEDOR = new CN_PROVEEDOR().Registrar(objPROVEEDOR, out mensaje);

                if (idgeneradoPROVEEDOR != 0)
                {
                    dataGridView1.Rows.Add(new object[] { txtid.Text,txtdocumento.Text,txtrazonsocial.Text,txtcorreo.Text,txttelefono.Text,                                             
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
                bool consulta = new CN_PROVEEDOR().Editar(objPROVEEDOR, out mensaje);
                if (consulta)
                {
                    DataGridViewRow row = dataGridView1.Rows[int.Parse(txtindicerow.Text)];
                    row.Cells["IdProveedor"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["RazonSocial"].Value = txtrazonsocial.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Telefono"].Value = txttelefono.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbestado.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).texto.ToString();
                }
                else { MessageBox.Show(mensaje); }
            }

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
                if (MessageBox.Show("Esta seguro de eliminar este Proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PROVEEDOR objPROVEEDOR = new PROVEEDOR()
                    {
                        IdProveedor = Convert.ToInt32(txtid.Text)
                    };
                    bool resultado = new CN_PROVEEDOR().Eliminar(objPROVEEDOR, out mensaje);

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            txtindicerow.Text = indice.ToString();
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtdocumento.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtrazonsocial.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
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

        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbbusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
