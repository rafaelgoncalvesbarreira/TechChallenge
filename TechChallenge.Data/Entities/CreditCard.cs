using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Domain.Entities
{
    public class CreditCard: BaseEntity
    {
        public Customer Customer{ get; set; }
        public long Number { get; set; }
        public int CVV { get; set; }
        public DateTime RegristrationDate { get; set; }
        public long Token { get; set; }
    }
}
