using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class FacturaDetalleNotFoundException : NotFoundException
    {
        public FacturaDetalleNotFoundException(int id)
        : base($"La Factura detalle con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
