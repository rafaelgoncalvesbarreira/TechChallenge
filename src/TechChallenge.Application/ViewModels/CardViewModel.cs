using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Application.ViewModels
{
    public class CardViewModel
    {
        public int CardId { get; set; }
        public long Token { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
