using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class VoucherNegocio
    {
        public bool ValidarCodigo(string codigo)
        {
            ConexionDB datos = new ConexionDB();
            try
            {
                datos.setearConsulta("SELECT IdCliente FROM Vouchers WHERE CodigoVoucher = @codigo");
                datos.setearParametro("@codigo", codigo);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // Si IdCliente es NULL, el voucher no esta usado
                    int? idCliente = datos.Lector.IsDBNull(0) ? (int?)null : datos.Lector.GetInt32(0);
                    return idCliente == null; // Si es NULL, esta disponible (no usado)
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void MarcarComoUsado(string codigo, int idCliente)
        {
            ConexionDB datos = new ConexionDB();
            try
            {
                // Asigno el IdCliente al voucher para marcarlo como usado
                datos.setearConsulta("UPDATE Vouchers SET IdCliente = @idCliente WHERE CodigoVoucher = @codigo AND IdCliente IS NULL");
                datos.setearParametro("@codigo", codigo);
                datos.setearParametro("@idCliente", idCliente);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
