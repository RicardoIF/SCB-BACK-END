using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class DependenciaNotFoundException : NotFoundException
    {
        public DependenciaNotFoundException(int dependenciaId)
        : base($"La dependencia con el Id: {dependenciaId} No se encuentra en la Base de datos.")
        {
        }
    }
}
