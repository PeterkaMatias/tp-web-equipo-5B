using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ParticipacionNegocio
    {
        public void RegistrarParticipacion(string codigoVoucher, int idPremio, int dniCliente)
        {
            var participacion = new Participacion
            {
                CodigoVoucher = codigoVoucher,
                Premio = new Premio { Id = idPremio },
                Cliente = new Cliente { Documento = dniCliente.ToString() },
                FechaCanje = DateTime.Now
            };

            ConexionDB.GuardarParticipacion(participacion);
        }
    }
}
