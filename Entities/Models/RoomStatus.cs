using Entities.Models.Base;

namespace Entities.Models
{
    public class RoomStatus : BaseModel
    {
        public DateTime StatusStart { get; set; }
        public DateTime StatusEnd { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int StatusId { get; set; }

    }
}