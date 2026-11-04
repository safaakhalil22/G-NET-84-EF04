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
    internal class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.FullName).IsRequired().HasMaxLength(150);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(150);
            builder.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(20);

            builder.HasData(
                new Manager { Id = 1, FullName = "Karim Hassan", Email = "karim.hassan@nationalbank.com", PhoneNumber = "01012345678", HireDate = new DateOnly(2015, 3, 1) },
                new Manager { Id = 2, FullName = "Laila Mostafa", Email = "laila.mostafa@nationalbank.com", PhoneNumber = "01123456789", HireDate = new DateOnly(2018, 9, 15) },
                new Manager { Id = 3, FullName = "Omar Fathy", Email = "omar.fathy@nationalbank.com", PhoneNumber = "01234567890", HireDate = new DateOnly(2020, 1, 10) }
            );
        }
    }
}
