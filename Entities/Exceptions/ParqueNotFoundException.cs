using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    internal class ParqueNotFoundException : NotFoundException
    {
        public ParqueNotFoundException(int id)
        : base($"El Parque con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
