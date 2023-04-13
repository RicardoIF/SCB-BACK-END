using Shared.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.AppointmentsDto
{
    public class CreateUpdateAppointment : DtoBase
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
        public int? RoomId { get; set; }
    }
}
