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
    public class CD_CLIENTE
    {
            public List<CLIENTE> Listar()
            {
                List<CLIENTE> lista = new List<CLIENTE>();

                try
                {

                    using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                    {
                    string query = "select IdCliente,Documento,Nombrecompleto,Correo,Telefono,Estado from CLIENTE";


                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.CommandType = CommandType.Text;
                        cn.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new CLIENTE()
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    NombreCompleto = dr["NombreCompleto"].ToString(),
                                    Documento = dr["Documento"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),
                                    
                                });


                            }
                        }

                    }

                }
                catch (Exception)
                {

                    lista = new List<CLIENTE>();
                }
                return lista;
            }


            public int Registrar(CLIENTE obj, out string mensaje)
            {
                int resultado = 0;
                mensaje = string.Empty;

                try
                {
                    using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                    {

                        SqlCommand cmd = new SqlCommand("RegistrarCliente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                        cmd.Parameters.AddWithValue("@Documento", obj.Documento);
                        cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                        cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                        cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                        cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        cn.Open();

                        cmd.ExecuteNonQuery();

                        resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);
                        mensaje = (cmd.Parameters["@Mensaje"].Value).ToString();
                    }
                }
                catch (Exception ex)
                {
                    resultado = 0;
                    mensaje = ex.Message;

                }
                return resultado;
            }

            public bool Editar(CLIENTE obj, out string mensaje)
            {
                bool resultado = false;
                mensaje = string.Empty;
                try
                {
                    using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                    {

                        SqlCommand cmd = new SqlCommand("EditarCliente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", obj.IdCliente);
                        cmd.Parameters.AddWithValue("@NombreCompleto", obj.NombreCompleto);
                        cmd.Parameters.AddWithValue("@Documento", obj.Documento);
                        cmd.Parameters.AddWithValue("@Correo", obj.Correo);
                        cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                        cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                        cmd.Parameters.Add("@Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        cn.Open();

                        cmd.ExecuteNonQuery();

                        resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
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

            public bool Eliminar(CLIENTE obj, out string mensaje)
            {
                bool resultado = false;
                mensaje = string.Empty;

                try
                {
                    using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                    {
                    string query = "delete from CLIENTE where IdCliente= @IdCliente";
                        SqlCommand cmd = new SqlCommand(query, cn);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@IdCliente", obj.IdCliente);


                        cmd.Parameters.Add("@Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                        cn.Open();

                      resultado= cmd.ExecuteNonQuery()>0? true:false;

                       
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
