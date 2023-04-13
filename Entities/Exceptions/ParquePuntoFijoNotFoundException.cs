using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    internal class ParquePuntoFijoNotFoundException : NotFoundException
    {
        public ParquePuntoFijoNotFoundException(int id)
        : base($"El Parque punto fijo con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
