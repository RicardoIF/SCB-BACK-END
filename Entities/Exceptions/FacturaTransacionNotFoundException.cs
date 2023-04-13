using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    internal class FacturaTransaccionNotFoundException : NotFoundException
    {
        public FacturaTransaccionNotFoundException(int id)
        : base($"La Factura transaccion con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
