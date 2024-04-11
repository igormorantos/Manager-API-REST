using Manager.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Infrastructure.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(80)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("email")
                .HasColumnType("VARCHAR(30)");

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(180)
                .HasColumnName("password")
                .HasColumnType("VARCHAR(180)");
        }
    }
}
