using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_Entidad;
using System.Data;
using System.Data.SqlClient;

namespace Capa_de_Datos
{
    public class CD_Rol
    {
        public List<ROL> Lista()
        {
            List<ROL> lista = new List<ROL>();

            try
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    string query = "select IdRol,Descripcion from ROL";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr =cmd.ExecuteReader() )
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ROL() 
                            { IdRol=Convert.ToInt32(dr["IdRol"]), 
                              Descripcion= dr["Descripcion"].ToString()
                            });
                        }
                    }
                    

                }

            }
            catch (Exception)
            {

                lista = new List<ROL>();
            }
            return lista;
        }
    }
}
