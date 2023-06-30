using Capa_de_Negocio;
using Capa_de_Presentacion.Utilidades;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentacion.ListasBuscadas
{
    public partial class listaproveedores : Form
    {

        // se crea un proveeedor para poder usarlo como su propiedad cuando se llame a este formulario
        // y el padre pueda acceder a esta propiedad y sus datos

        public PROVEEDOR proveedorselecionado { get; set; }
        public listaproveedores()
        {
            InitializeComponent();
        }

        private void listaproveedores_Load(object sender, EventArgs e)
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

            List<PROVEEDOR> listaProveedores = new CN_PROVEEDOR().Listar();
            foreach (PROVEEDOR PROVEEDOR in listaProveedores)
            {
                dataGridView1.Rows.Add(new object[] {PROVEEDOR.IdProveedor,PROVEEDOR.Documento,PROVEEDOR.RazonSocial                                             
                                                });
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
       

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int icolum = e.ColumnIndex;

            if(iRow>=0 && icolum > 0)
            {
                proveedorselecionado = new PROVEEDOR()
                {
                    IdProveedor = Convert.ToInt32(dataGridView1.Rows[iRow].Cells["IdProveedor"].Value),
                    Documento = dataGridView1.Rows[iRow].Cells["Documento"].Value.ToString(),
                    RazonSocial = dataGridView1.Rows[iRow].Cells["RazonSocial"].Value.ToString()
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
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
