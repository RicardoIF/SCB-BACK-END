using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class GrupoOcupacionalCollectionBadRequest : BadRequestException
    {
        public GrupoOcupacionalCollectionBadRequest() 
            : base("La coleccion enviada desde el cliente es nula.") { }
    }
}
