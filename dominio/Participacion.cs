using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Participacion
    {
        public int Id { get; set; }
        public string CodigoVoucher { get; set; }
        public int IdPremio { get; set; }
        public int DniCliente { get; set; }
        public DateTime Fecha { get; set; }
    }
}
