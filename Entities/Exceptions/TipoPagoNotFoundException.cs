using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    internal class TipoPagoNotFoundException : NotFoundException
    {
        public TipoPagoNotFoundException(int id)
        : base($"El Tipo de pago con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
