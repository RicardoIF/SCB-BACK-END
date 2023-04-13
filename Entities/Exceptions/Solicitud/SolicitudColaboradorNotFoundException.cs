using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Solicitud
{
    public class SolicitudColaboradorNotFoundException : NotFoundException
    {
        public SolicitudColaboradorNotFoundException(int id) : base($"La Solicitud con el Id: {id} No se encuentra en la Base de datos.")
        {

        }
    }
}
