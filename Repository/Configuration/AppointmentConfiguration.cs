using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("ID");
            builder.HasOne(x => x.Room).WithMany(x => x.Appointments)
                .HasForeignKey(x => x.RoomId).IsRequired(false);
            builder.Property(x => x.CheckIn).IsRequired(false);
            builder.Property(x => x.CheckOut).IsRequired(false);
            builder.Property(x => x.StudentName).HasColumnType("nvarchar(80)").IsRequired(false);
            builder.Property(x => x.StudentLastName).HasColumnType("nvarchar(80)").IsRequired(false);
        }
    }
}
