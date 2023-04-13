using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class CargoDepartamentoNotFoundException : NotFoundException
    {
        public CargoDepartamentoNotFoundException(int id)
        : base($"El CargoDepartamento con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
