using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IAuditDbContext
    {
        DbSet<Audit> Audit { get; set; }
        ChangeTracker ChangeTracker { get; }
    }
}
