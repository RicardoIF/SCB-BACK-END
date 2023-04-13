using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Solicitud
{
    public class TipoPermisoNotFoundException : NotFoundException
    {
        public TipoPermisoNotFoundException(int id)
        : base($"El Tipo de Permiso con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}
