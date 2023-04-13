using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class TipoDepartamentoNotFoundException : NotFoundException
    {
        public TipoDepartamentoNotFoundException(int id) : base($"El Tipo Departamento con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}