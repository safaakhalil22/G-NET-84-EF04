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
    internal class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(ca => new { ca.CustomerId, ca.AccountNumber });

            builder.Property(ca => ca.AccountNumber).HasMaxLength(20);
            builder.Property(ca => ca.OwnershipType).HasConversion<string>().HasMaxLength(20);
            builder.Property(ca => ca.AccountStatus).HasConversion<string>().HasMaxLength(20);

            builder.HasOne(ca => ca.Customer)
                   .WithMany(c => c.CustomerAccounts)
                   .HasForeignKey(ca => ca.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ca => ca.Account)
                   .WithMany(a => a.CustomerAccounts)
                   .HasForeignKey(ca => ca.AccountNumber)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new CustomerAccount { CustomerId = 1, AccountNumber = "2001-CUR", OwnershipType = OwnershipType.Primary, OwnershipStartDate = new DateOnly(2024, 1, 15), AccountStatus = AccountStatus.Active },
                new CustomerAccount { CustomerId = 2, AccountNumber = "3000-BUS", OwnershipType = OwnershipType.Primary, OwnershipStartDate = new DateOnly(2024, 3, 20), AccountStatus = AccountStatus.Active }
            );
        }
    }
}
