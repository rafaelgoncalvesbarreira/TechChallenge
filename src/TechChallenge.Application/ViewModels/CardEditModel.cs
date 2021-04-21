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
            try
            {
                Assert(CVV_SIZE, (int)Math.Ceiling(Math.Log10(CVV)));
                Assert(CARDNUMBER_SIZE, (int)Math.Ceiling(Math.Log10(CardNumber)));

                return true;
            }
            catch(AssertValidationException ex)
            {
                return false;
            }
        }

        private void Assert(int max, int actual)
        {
            if(actual > max)
            {
                throw new AssertValidationException("Expected != actual");
            }
        }
    }
}
