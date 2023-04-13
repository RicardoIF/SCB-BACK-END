using Entities.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class FacturaNotFoundException : NotFoundException
    {
        public FacturaNotFoundException(int id)
        : base($"La Factura con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
