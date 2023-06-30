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
    public class CD_COMPRA
    {
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;
            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {
                    string query = "select count(*)+1 from COMPRA ";

                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch
                {
                    idcorrelativo = 0;

                }
            }
            return idcorrelativo;
        }



        public bool Registrar(COMPRA obj, DataTable detallecompra, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("RegistrarCompra", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.Usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@IdProveedor", obj.Proveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("@TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("@NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("@DetalleCompra", detallecompra);

                    cmd.Parameters.Add("@Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cn.Open();
                    cmd.ExecuteNonQuery();

                    Resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    Mensaje = cmd.Parameters["@Mensaje"].ToString();



                }
                catch (Exception ex)
                {

                    Resultado = false;
                    Mensaje = ex.Message;
                }
            }
            return Resultado;
        }


        public COMPRA ObtenerCompra(string numerodocumento)
        {
            COMPRA obj= null;

            try
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine(" select c.IdCompra,u.NombreCompleto,pr.Documento,pr.RazonSocial,c.TipoDocumento, ");
                    query.AppendLine(" c.NumeroDocumento,c.MontoTotal,CONVERT(char(10),c.FechaCreacion,103)[FechaRegistro] from COMPRA c inner join USUARIO u on u.IdUsuario= c.IdUsuario");
                    query.AppendLine("inner join PROVEEDOR pr on pr.IdProveedor=c.IdProveedor where c.NumeroDocumento=@numero");

                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.AddWithValue("@numero",numerodocumento);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            obj = new COMPRA()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"].ToString()),
                                Usuario = new USUARIO() { NombreCompleto = dr["NombreCompleto"].ToString() },
                                Proveedor = new PROVEEDOR() { Documento = dr["Documento"].ToString(), RazonSocial = dr["RazonSocial"].ToString() },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal =Convert.ToDecimal( dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };

                        }
                    }

                }

            }
            catch (Exception)
            {
                obj = new COMPRA();
            }

            return obj;
        }

        public List<DETALLE_COMPRA> ObtenerDetalleCompra(int IdCompra)
        {
            List<DETALLE_COMPRA> lista = new List<DETALLE_COMPRA>();

            try
            {

                using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
                {
                    StringBuilder query = new StringBuilder();

                    query.AppendLine(" select p.Nombre, dc.PrecioCompra,dc.Cantidad,dc.MontoTotal from DETALLE_COMPRA dc ");
                    query.AppendLine(" inner join PRODUCTO p on dc.IdProducto=p.IdProducto where dc.IdCompra=@IdCompra");

                    SqlCommand cmd = new SqlCommand(query.ToString(), cn);
                    cmd.Parameters.AddWithValue("@IdCompra", IdCompra);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new DETALLE_COMPRA()
                            {
                                Producto= new PRODUCTO() { Nombre=dr["Nombre"].ToString()},
                                PrecioCompra= Convert.ToDecimal( dr["PrecioCompra"].ToString()),
                                Cantidad= Convert.ToInt32(dr["Cantidad"].ToString()),
                                Montototal= Convert.ToDecimal(dr["MontoTotal"].ToString()),
                            });
                            

                        }
                    }
                     
                }

            }
            catch (Exception)
            {
                lista = new List<DETALLE_COMPRA>();
            }

            return lista;
        }

        public List<COMPRA> listaNumero_compra()
        {
            List<COMPRA> lista = new List<COMPRA>();

            using (SqlConnection cn = new SqlConnection(CONEXION.cadena))
            {
                try
                {
                    string query = "select IdCompra,NumeroDocumento from COMPRA";

                    SqlCommand cmd = new SqlCommand(query,cn);
                    cmd.CommandType = CommandType.Text;
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new COMPRA() {
                            
                            IdCompra= Convert.ToInt32(dr["IdCompra"].ToString()),
                            NumeroDocumento= dr["NumeroDocumento"].ToString()

                            });
                        }
                    }
                }
                catch
                {
                    lista = new List<COMPRA>();                
                }
            }

                return lista;
        }
    }   
}
