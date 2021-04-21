using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Application.ViewModels
{
    public class TokenValidationEditModel
    {
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public long Token { get; set; }
        public int CVV { get; set; }
    }
}
