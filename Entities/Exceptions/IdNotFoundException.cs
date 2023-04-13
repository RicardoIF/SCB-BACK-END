using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class IdNotFoundException : NotFoundException
    {
        public IdNotFoundException(int id) : base($"El Registro con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
