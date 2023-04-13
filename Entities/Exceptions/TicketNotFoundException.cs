using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class TicketNotFoundException : NotFoundException
    {
        public TicketNotFoundException(int id)
        : base($"El Ticket con el Id: {id} No se encuentra en la Base de datos.")
        {
        }
    }
}