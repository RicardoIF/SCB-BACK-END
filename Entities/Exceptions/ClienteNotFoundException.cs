using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class ClienteNotFoundException : NotFoundException
    {
        public ClienteNotFoundException(int id)
        : base($"El Cliente con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
