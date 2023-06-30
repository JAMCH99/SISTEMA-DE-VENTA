using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Capa_Entidad;

namespace Capa_de_Datos
{
    public class CD_USUARIO
    {
        public List<USUARIO> Listar()
        {
            List<USUARIO> lista = new List<USUARIO>();

            try
            {

                 using(SqlConnection cn= new SqlConnection(CONEXION.cadena))
                 {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdUsuario, Documento, NombreCompleto, Correo, Clave, Estado, r.IdRol,r.Descripcion from USUARIO u");
                    query.AppendLine("inner join ROL r on u.IdRol = r.IdRol");
                    

                    SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using(SqlDataReader dr= cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                        lista.Add(new USUARIO()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NombreCompleto = dr["NombreCompleto"].ToString(),
                            Documento = dr["Documento"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            Estado=Convert.ToBoolean(dr["Estado"]),
                            Rol=(new ROL() {IdRol = Convert.ToInt32(dr["IdRol"]),Descripcion= dr["Descripcion"].ToString()})
                        });
                        
    
                        }
                    }
                
                 }
                  
            }
            catch(Exception ) 
            { 

                lista = new List<USUARIO>();
            }
                 return lista;
        }


        public int Registrar(USUARIO obj, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

          try
          {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("RegistrarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("@IdRol", obj.Rol.IdRol);
                    cmd.Parameters.AddWithValue("@Estado",obj.Estado);

                cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                cn.Open();

                cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                    mensaje =(cmd.Parameters["@Mensaje"].Value).ToString();
              }
          }
          catch (Exception ex)
          {
                resultado = 0;
                mensaje = ex.Message;
                    
          }
                return resultado;
        }

        public bool Editar(USUARIO obj, out string mensaje)
        {
            bool resultado =false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("EditarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                    cmd.Parameters.AddWithValue("@Documento", obj.Documento);
                    cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("@Clave", obj.Clave);
                    cmd.Parameters.AddWithValue("@IdRol", obj.Rol.IdRol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    mensaje = (cmd.Parameters["@Mensaje"].Value).ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }

        public bool Eliminar(USUARIO obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("EliminarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    

                    cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["@Respuesta"].Value);
                    mensaje = (cmd.Parameters["@Mensaje"].Value).ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;

            }
            return resultado;
        }
    }


}
