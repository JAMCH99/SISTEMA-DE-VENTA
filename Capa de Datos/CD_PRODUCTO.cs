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
    public class CD_PRODUCTO
    {
        public List<PRODUCTO> ListarProducto()
        {
            List<PRODUCTO> lista = new List<PRODUCTO>();

            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProducto, Codigo, Nombre, p.Descripcion,c.IdCategoria,c.Descripcion[DescripcionCategoria],Stock,PrecioCompra,PrecioVenta,p.Estado from PRODUCTO p");
                    query.AppendLine("inner join CATEGORIA c on p.IdCategoria = c.IdCategoria");

                    SqlCommand cmd = new SqlCommand(query.ToString(),cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr= cmd.ExecuteReader()) 
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PRODUCTO()
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Codigo = dr["Codigo"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Categoria = new CATEGORIA()
                                    {
                                        IdCategoria= Convert.ToInt32(dr["IdCategoria"]),
                                        Descripcion= dr["DescripcionCategoria"].ToString()
                                    },
                                    Stock= Convert.ToInt32(dr["Stock"]),
                                    PrecioCompra=Convert.ToDecimal(dr["PrecioCompra"]),
                                    PrecioVenta= Convert.ToDecimal(dr["PrecioVenta"]),
                                    Estado= Convert.ToBoolean(dr["Estado"])
                                }
                                    );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<PRODUCTO>();
            }

        return lista;
        }
        

        public int Registrar(PRODUCTO obj, out string mensaje)
        {
            int resultado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("RegistrarProducto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", obj.Categoria.IdCategoria);
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

        public bool Editar(PRODUCTO obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("EditarProducto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("@Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", obj.Categoria.IdCategoria);
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

        public bool Eliminar(PRODUCTO obj, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {

                    SqlCommand cmd = new SqlCommand("EliminarProducto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", obj.IdProducto);


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
    }

}


