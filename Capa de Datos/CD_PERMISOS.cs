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
    public class CD_PERMISOS
    {
        public List<PERMISO> Ver_Permisos(int idusuario)
        {
            List<PERMISO> lista = new List<PERMISO>();

            try 
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.IdRol, p.NombreMenu from PERMISO p");
                    query.AppendLine("inner join ROL r on r.IdRol=p.IdRol");
                    query.AppendLine("inner join USUARIO u on u.IdRol= r.IdRol");
                    query.AppendLine("where u.IdUsuario= @idusuario");


                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.AddWithValue("@idusuario", idusuario);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PERMISO()
                            {
                                Rol = new ROL() { IdRol = Convert.ToInt32(dr["IdRol"]) },
                                NombreMenu = dr["NombreMenu"].ToString()
                            }) ;


                        }
                    }

                }

            }
            catch (Exception)
            {

                lista = new List<PERMISO>();
            }
            return lista;
        }
    }
}

