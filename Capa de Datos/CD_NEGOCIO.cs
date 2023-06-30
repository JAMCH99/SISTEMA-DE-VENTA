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
    public class CD_NEGOCIO
    {
        public NEGOCIO ObtenerDatos()
        {
            NEGOCIO obj = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    string query = "select IdNegocio,Nombre,Ruc,Direccion from NEGOCIO";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new NEGOCIO()
                            {
                                IdNegocio = Convert.ToInt32(dr["IdNegocio"]),
                                Nombre = dr["Nombre"].ToString(),
                                Ruc = dr["Ruc"].ToString(),
                                Direccion = dr["Direccion"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                obj = new NEGOCIO();
            }

            return obj;
        }

        public bool GuardarDatos(NEGOCIO obj, out string mensaje)
        {
            bool Resultado = true;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Update NEGOCIO set");
                    query.AppendLine("Nombre= @Nombre,");
                    query.AppendLine("Ruc=@Ruc,");
                    query.AppendLine("Direccion=@Direccion");
                    query.AppendLine("where IdNegocio= 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Ruc", obj.Ruc);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cn.Open();

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo guardar";
                        Resultado = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Resultado = false;

                mensaje = ex.Message;
            }


            return Resultado;
        }


        public byte[] ObtenerLogo(out bool Resultado)
        {
            Resultado = true;
            byte[] logo = new byte[0];

            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    string query = "select Logo from NEGOCIO where IdNegocio=1";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            logo = (byte[])(dr["Logo"]);
                        }
                    }

                }
            }
            catch (Exception)
            {
                Resultado = false;
                logo = new byte[0];
            }

            return logo;
        }

        public bool Actualizar_Logo(byte[] imagen, out string mensaje)
        {
            mensaje = string.Empty;
           bool Resultado = true;
            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("Update NEGOCIO set");
                    query.AppendLine("Logo= @Logo");
                    query.AppendLine("where IdNegocio= 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Logo", imagen);

                    cn.Open();

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        mensaje = "No se pudo actualizar el logo";
                        Resultado = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Resultado = false;
                mensaje = ex.Message;
            }

            return Resultado;
        }
    }
}
