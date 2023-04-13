using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class GrupoOcupacionalNotFoundException : NotFoundException
    {
        public GrupoOcupacionalNotFoundException(int id)
            : base($"El Grupo Ocupacional con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
