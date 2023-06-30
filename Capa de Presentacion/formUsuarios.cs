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
    public partial class formUsuarios : Form
    {
        public formUsuarios()
        {
            InitializeComponent();
        }

        private void formUsuarios_Load(object sender, EventArgs e)
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

            foreach (ROL item in lista)
            {
                cmbrol.Items.Add(new OpcionCombo() { valor = item.IdRol, texto = item.Descripcion });
            }

            cmbrol.DisplayMember = "texto";
            cmbrol.ValueMember = "valor";
            cmbrol.SelectedIndex = 0;

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

            List<USUARIO> listaUsuario = new CN_USUARIO().Listar();
            foreach (USUARIO usuario in listaUsuario)
            {
                dataGridView1.Rows.Add(new object[] {usuario.IdUsuario,usuario.Documento,usuario.NombreCompleto,usuario.Correo,usuario.Clave,
                                                usuario.Rol.IdRol,
                                                usuario.Rol.Descripcion,
                                                usuario.Estado==true ? 1:0,
                                                usuario.Estado== true? "Activo":"Inactivo"
                                                });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {

            string mensaje = string.Empty;
            USUARIO objusuario = new USUARIO()
            {
                IdUsuario = Convert.ToInt32(txtid.Text),
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombre.Text,
                Correo = txtcorreo.Text,
                Clave = txtclave.Text,
                Rol = new ROL() { IdRol = Convert.ToInt32(((OpcionCombo)cmbrol.SelectedItem).valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cmbestado.SelectedItem).valor) == 1 ? true : false

            };
            if (objusuario.IdUsuario == 0)
            {


                int idgeneradousuario = new CN_USUARIO().Registrar(objusuario, out mensaje);

                if (idgeneradousuario != 0)
                {
                    dataGridView1.Rows.Add(new object[] { txtid.Text,txtdocumento.Text,txtnombre.Text,txtcorreo.Text,txtclave.Text,
                                                ((OpcionCombo)cmbrol.SelectedItem).valor.ToString(),
                                                ((OpcionCombo)cmbrol.SelectedItem).texto.ToString(),
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
                bool consulta = new CN_USUARIO().Editar(objusuario, out mensaje);
                if (consulta)
                {
                    DataGridViewRow row = dataGridView1.Rows[int.Parse(txtindicerow.Text)];
                    row.Cells["IdUsuario"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtdocumento.Text;
                    row.Cells["NombreCompleto"].Value = txtnombre.Text;
                    row.Cells["Correo"].Value = txtcorreo.Text;
                    row.Cells["Clave"].Value = txtclave.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)cmbrol.SelectedItem).valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cmbrol.SelectedItem).texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cmbestado.SelectedItem).valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cmbestado.SelectedItem).texto.ToString();
                }
                else { MessageBox.Show(mensaje); }
            }
        }

        private void limpiar()
        {
            txtindicerow.Text = "-1"; txtid.Text = "0"; txtdocumento.Text = ""; txtnombre.Text = "";
            txtcorreo.Text = ""; txtclave.Text = ""; txtconfirmarclave.Text = "";
            cmbrol.SelectedIndex = 0;
            cmbestado.SelectedIndex = 0;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;
            txtindicerow.Text = indice.ToString();
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtdocumento.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtnombre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtcorreo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtclave.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtconfirmarclave.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

            foreach (OpcionCombo oc in cmbrol.Items)
            {
                if(Convert.ToInt32(oc.valor)== Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value))
                {
                    cmbrol.SelectedIndex = cmbrol.Items.IndexOf(oc);
                }
            }
            foreach (OpcionCombo oc in cmbestado.Items)
            {
                if (Convert.ToInt32(oc.valor) == Convert.ToInt32(dataGridView1.CurrentRow.Cells[7].Value))
                {
                    cmbestado.SelectedIndex = cmbestado.Items.IndexOf(oc);
                }
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
                if (MessageBox.Show("Esta seguro de eliminar este usuario?","Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    USUARIO objusuario = new USUARIO()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text)
                    };
                    bool resultado = new CN_USUARIO().Eliminar(objusuario, out mensaje);

                    if (resultado)
                    {
                        dataGridView1.Rows.RemoveAt(int.Parse(txtindicerow.Text));
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show(mensaje,"Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Exclamation );
                    }
                }       
            }
        }

        private void buscador_Click(object sender, EventArgs e)
        {
            string columna = ((OpcionCombo)cmbbusqueda.SelectedItem).valor.ToString();

            if (dataGridView1.Rows.Count > 0)
            {
                foreach(DataGridViewRow row in dataGridView1.Rows)
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
    }

}