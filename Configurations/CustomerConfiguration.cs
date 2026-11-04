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
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName).IsRequired().HasMaxLength(150);
            builder.Property(c => c.NationalId).IsRequired().HasMaxLength(14);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(150);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(250);
            builder.Property(c => c.CustomerType).HasConversion<string>().HasMaxLength(20);

            builder.HasIndex(c => c.NationalId).IsUnique();

            builder.HasData(
                new Customer { Id = 1, FullName = "Ahmed Ali", NationalId = "29001011234567", DateOfBirth = new DateOnly(1990, 1, 1), Email = "ahmed.ali@gmail.com", PhoneNumber = "01011112222", Address = "10-Street-Cairo-Egypt", CustomerType = CustomerType.Individual },
                new Customer { Id = 2, FullName = "Nile Trading LLC", NationalId = "30506151234567", DateOfBirth = new DateOnly(1985, 6, 15), Email = "info@niletrading.com", PhoneNumber = "01122223333", Address = "25-Street-Alexandria-Egypt", CustomerType = CustomerType.Business }
            );
        }
    }
}
