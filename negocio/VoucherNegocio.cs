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
            // Acceso a datos para verificar si el voucher existe y no fue usado
            return ConexionDB.ExisteVoucherValido(codigo);
        }

        public void MarcarComoUsado(string codigo)
        {
            ConexionDB.MarcarVoucherUsado(codigo);
        }
    }
}
