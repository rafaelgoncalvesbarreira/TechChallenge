using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Application.Validation;

namespace TechChallenge.Application.ViewModels
{
    public class CardEditModel: IValidable
    {
        private const int CVV_SIZE = 5;
        private const int CARDNUMBER_SIZE = 16;
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }

        public bool isValid()
        {
            if(CardNumber <= 0 || (int)Math.Ceiling(Math.Log10(CardNumber)) > CARDNUMBER_SIZE)
            {
                return false;
            }
            if(CVV <=0 || (int)Math.Ceiling(Math.Log10(CVV)) > CVV_SIZE)
            {
                return false;
            }

            return true;
        }
    }
}
