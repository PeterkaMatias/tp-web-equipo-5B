using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClienteNegocio
    {
        public Cliente BuscarPorDNI(int dni)
        {
            return ConexionDB.ObtenerClientePorDNI(dni);
        }

        public void Guardar(Cliente cliente)
        {
            ConexionDB.GuardarCliente(cliente);
        }
    }
}
