using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.RoomStatusDto
{
    public class CreateUpdateRoomStatusDto
    {
        public DateTime StatusStart { get; set; }
        public DateTime StatusEnd { get; set; }
        public int RoomId { get; set; }
        public int StatusId { get; set; }
    }
}
