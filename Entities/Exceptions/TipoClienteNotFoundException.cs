using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class TipoClienteNotFoundException : NotFoundException
    {
        public TipoClienteNotFoundException(int id)
        : base($"El Tipo de cliente con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
