using Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.RoomDto
{
    public class RoomDto : DtoBase
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
    }
}
