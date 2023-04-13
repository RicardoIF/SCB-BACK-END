using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class DepartamentoNotFoundException : NotFoundException
    {
        public DepartamentoNotFoundException(int departamentoId)
        : base($"El departamento con el Id: {departamentoId} No se encuentra en la Base de datos.")
        {
        }
    }

}
