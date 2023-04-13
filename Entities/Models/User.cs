using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsRoot { get; set; }
        public bool IsLocked { get; set; }
        public DateTimeOffset RegistrationDate { get; set; }
        public DateTimeOffset? ModificationDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdateBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
