using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.Base;

namespace Entities.Models
{
    public class Room : BaseModel
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
        public ICollection<RoomStatus> RoomStatus { get; set; }
        public ICollection<Appointment> Appointments { get; set; }


    }
}
