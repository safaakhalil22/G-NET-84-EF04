using G_NET_84_EF04.Models;
using G_NET_84_EF04.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_84_EF04.Configurations
{
    internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Code);

            builder.Property(b => b.Code).HasMaxLength(10);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Address).IsRequired().HasMaxLength(250);
            builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(20);

            builder.HasOne(b => b.Manager)
                   .WithOne(m => m.Branch)
                   .HasForeignKey<Branch>(b => b.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.Accounts)
                   .WithOne(a => a.Branch)
                   .HasForeignKey(a => a.BranchCode)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Branch { Code = "CAI-01", Name = "Cairo Main Branch", Address = "15 Tahrir Square, Cairo", PhoneNumber = "0225780001", ManagerId = 1 },
                new Branch { Code = "ALX-01", Name = "Alexandria Branch", Address = "42 Corniche Road, Alexandria", PhoneNumber = "0334870002", ManagerId = 2 },
                new Branch { Code = "GIZ-01", Name = "Giza Branch", Address = "8 Pyramids Street, Giza", PhoneNumber = "0233850003", ManagerId = 3 }
            );
        }
    }
}
