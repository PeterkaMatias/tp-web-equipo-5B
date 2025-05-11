using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Participacion
    {
        public string CodigoVoucher { get; set; }
        public Cliente Cliente { get; set; }
        public Premio Premio { get; set; }
        public DateTime FechaCanje { get; set; }
    }
}
