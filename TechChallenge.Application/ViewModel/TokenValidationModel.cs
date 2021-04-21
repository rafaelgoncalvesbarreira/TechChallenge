using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Application.ViewModel
{
    public class TokenValidationModel
    {
        public int CustomerId { get; set; }
        public int CardId { get; set; }
        public long Token { get; set; }
        public int CVV { get; set; }
    }
}
