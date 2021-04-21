using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Application.ViewModels;
using Xunit;

namespace TechChallenge.Application.Test.ViewModels
{
    public class CreditCardValidationTestes
    {
        [Fact]
        public void ValidCreditCard()
        {
            var model = new CardEditModel
            {
                CardNumber = 123456,
                CustomerId = 1,
                CVV = 654
            };

            Assert.True(model.isValid());
        }

        [Fact]
        public void InvalidNumberCreditCard()
        {
            var model = new CardEditModel
            {
                CardNumber = 12345678901234567,
                CustomerId = 1,
                CVV = 654
            };

            Assert.False(model.isValid());
        }

        [Fact]
        public void InvalidCVVCreditCard()
        {
            var model = new CardEditModel
            {
                CardNumber = 123456789,
                CustomerId = 1,
                CVV = 654321
            };

            Assert.False(model.isValid());
        }
    }
}
