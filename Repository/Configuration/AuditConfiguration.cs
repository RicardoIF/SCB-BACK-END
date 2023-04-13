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
    internal class AuditConfig : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> entity)
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("tbl_test_audit_trail");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.AuditDateTimeUtc)
                .HasColumnName("audit_datetime_utc");
            entity.Property(e => e.AuditType)
                .HasColumnName("audit_type");
            entity.Property(e => e.AuditUser)
                .HasColumnName("audit_user");
            entity.Property(e => e.TableName)
                .HasColumnName("table_name");
            entity.Property(e => e.KeyValues)
                .HasColumnName("key_values");
            entity.Property(e => e.OldValues)
                .HasColumnName("old_values");
            entity.Property(e => e.NewValues)
                .HasColumnName("new_values");
            entity.Property(e => e.ChangedColumns)
                .HasColumnName("changed_columns");
        }
    }
}
