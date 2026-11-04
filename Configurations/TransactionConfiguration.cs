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
    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionNumber);

            builder.Property(t => t.Amount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.TransactionType).HasConversion<string>().HasMaxLength(20);
            builder.Property(t => t.Note).HasMaxLength(250);
            builder.Property(t => t.AccountNumber).IsRequired().HasMaxLength(20);

            builder.HasOne(t => t.Account)
                   .WithMany(a => a.Transactions)
                   .HasForeignKey(t => t.AccountNumber)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
