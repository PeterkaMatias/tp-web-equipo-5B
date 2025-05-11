using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ConexionDB
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector => lector;

        public ConexionDB()
        {
            conexion = new SqlConnection("server=LOCALHOST; database=PROMOS_DB; integrated security=true");
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public static bool ExisteVoucherValido(string codigo)
        {
            ConexionDB db = new ConexionDB();
            try
            {
                db.setearConsulta("SELECT COUNT(*) FROM Vouchers WHERE CodigoVoucher = @codigo AND IdCliente IS NULL");
                db.setearParametro("@codigo", codigo);
                db.ejecutarLectura();

                if (db.Lector.Read())
                {
                    int count = db.Lector.GetInt32(0);
                    return count > 0;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.cerrarConexion();
            }
        }

        public static void MarcarVoucherUsado(string codigo)
        {
            ConexionDB datos = new ConexionDB();
            try
            {
                datos.setearConsulta("UPDATE Vouchers SET FechaCanje = GETDATE() WHERE CodigoVoucher = @codigo");
                datos.setearParametro("@codigo", codigo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public static List<Premio> ListarPremios()
        {
            List<Premio> lista = new List<Premio>();
            ConexionDB datos = new ConexionDB();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Descripcion, Precio FROM Articulos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Premio p = new Premio
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        Precio = (decimal)datos.Lector["Precio"]
                    };

                    lista.Add(p);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public static Cliente ObtenerClientePorDNI(int dni)
        {
            ConexionDB datos = new ConexionDB();

            try
            {
                datos.setearConsulta("SELECT * FROM Clientes WHERE Documento = @dni");
                datos.setearParametro("@dni", dni.ToString());
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Cliente
                    {
                        Id = (int)datos.Lector["Id"],
                        Documento = datos.Lector["Documento"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Apellido = datos.Lector["Apellido"].ToString(),
                        Email = datos.Lector["Email"].ToString(),
                        Direccion = datos.Lector["Direccion"].ToString(),
                        Ciudad = datos.Lector["Ciudad"].ToString(),
                        CP = (int)datos.Lector["CP"]
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public static void GuardarCliente(Cliente cliente)
        {
            ConexionDB datos = new ConexionDB();

            try
            {
                datos.setearConsulta(@"INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP)
                               VALUES (@doc, @nom, @ape, @mail, @dir, @ciudad, @cp)");
                datos.setearParametro("@doc", cliente.Documento);
                datos.setearParametro("@nom", cliente.Nombre);
                datos.setearParametro("@ape", cliente.Apellido);
                datos.setearParametro("@mail", cliente.Email);
                datos.setearParametro("@dir", cliente.Direccion);
                datos.setearParametro("@ciudad", cliente.Ciudad);
                datos.setearParametro("@cp", cliente.CP);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public static void GuardarParticipacion(Participacion participacion)
        {
            ConexionDB datos = new ConexionDB();

            try
            {
                datos.setearConsulta(@"UPDATE Vouchers
                               SET IdCliente = @idCliente,
                                   IdArticulo = @idArticulo,
                                   FechaCanje = GETDATE()
                               WHERE CodigoVoucher = @codigo");
                datos.setearParametro("@idCliente", participacion.Cliente.Id);
                datos.setearParametro("@idArticulo", participacion.Premio.Id);
                datos.setearParametro("@codigo", participacion.CodigoVoucher);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
