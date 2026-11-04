using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_84_EF04.Models
{
    internal class Branch
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public int ManagerId { get; set; }
        public Manager Manager { get; set; } = null!;

        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
    }
}
