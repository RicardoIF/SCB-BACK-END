using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Base
{
    public interface IBaseModel
    {
        public int Id { get; set; }
        public bool? Estatus { get; set; }
    }
}
