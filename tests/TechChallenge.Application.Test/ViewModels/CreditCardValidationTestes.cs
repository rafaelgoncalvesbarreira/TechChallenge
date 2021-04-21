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
        public void CreditCardNumberTooBig()
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
        public void CreditCardNumberTooSmall()
        {
            var model = new CardEditModel
            {
                CardNumber = 0,
                CustomerId = 1,
                CVV = 654
            };

            Assert.False(model.isValid());
        }

        [Fact]
        public void CreditCardCVVTooBig()
        {
            var model = new CardEditModel
            {
                CardNumber = 123456789,
                CustomerId = 1,
                CVV = 654321
            };

            Assert.False(model.isValid());
        }

        [Fact]
        public void CreditCardCVVTooSmall()
        {
            var model = new CardEditModel
            {
                CardNumber = 12345678,
                CustomerId = 1,
                CVV = 0
            };

            Assert.False(model.isValid());
        }
    }
}
