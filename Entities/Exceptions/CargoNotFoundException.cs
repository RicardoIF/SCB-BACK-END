using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CargoNotFoundException : NotFoundException
    {
        public CargoNotFoundException(int id)
        : base($"El Cargo con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
