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
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountNumber);

            builder.Property(a => a.AccountNumber).HasMaxLength(20);
            builder.Property(a => a.CurrentBalance).HasColumnType("decimal(18,2)");
            builder.Property(a => a.AccountType).HasConversion<string>().HasMaxLength(20);
            builder.Property(a => a.BranchCode).IsRequired().HasMaxLength(10);

            builder.HasData(
                new Account { AccountNumber = "2001-CUR", CurrentBalance = 15400m, AccountType = AccountType.Current, OpeningDate = new DateOnly(2024, 1, 15), BranchCode = "CAI-01" },
                new Account { AccountNumber = "3000-BUS", CurrentBalance = 120000m, AccountType = AccountType.Business, OpeningDate = new DateOnly(2024, 3, 20), BranchCode = "ALX-01" }
            );
        }
    }
}
