using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Base
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }
        //public DateTime FechaRegistro { get; set; }
        //public DateTime FechaModificacion { get; set; }
        public bool? Estatus { get; set; }
        //public int? CreadoPor { get; set; }
        //public int? ModificadoPor { get; set; }
    }
}
