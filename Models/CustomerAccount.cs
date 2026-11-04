using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G_NET_84_EF04.Models.Enums;

namespace G_NET_84_EF04.Models
{
    internal class CustomerAccount
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;
        public Account Account { get; set; } = null!;

        public OwnershipType OwnershipType { get; set; }
        public DateOnly OwnershipStartDate { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
