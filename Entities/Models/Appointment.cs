using Entities.Models.Base;

namespace Entities.Models
{
    public class Appointment : BaseModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Host { get; set; }
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentLastName { get; set; }
        public  int? RoomId { get; set; }

        public Room Room { get; set; }
     

    }
}