using System;
using System.Collections.Generic;
using System.Text;
using TechChallenge.Application.Contract;
using TechChallenge.Application.Services;
using Xunit;
using Moq;
using TechChallenge.Data.Contract;
using TechChallenge.Domain.Entities;
using System.Linq;
using Microsoft.Extensions.Logging;
using TechChallenge.Application.ViewModel;
using MockQueryable.Moq;

namespace TechChallenge.Application.Test.Services
{
    public class CardServicesTest
    {
        private readonly CardServices cardService;
        public CardServicesTest()
        {
            cardService = new CardServices(GetRepository(), new MockLogger<CreditCard>());
        }

        [Fact]
        public void GenerateSucessefulToken()
        {
            var token = cardService.GenerateToken(1234567890, 154);
            Assert.Equal(9078, token);
        }

        [Fact]
        public void GenerateTokenLess4digits()
        {
            var token = cardService.GenerateToken(123, 2);
            Assert.Equal(231, token);
        }

        [Fact]
        public async void ValidateTokenTrue()
        {
            TokenValidationEditModel model = new TokenValidationEditModel
            {
                CardId = 1,
                CustomerId = 1,
                CVV = 154,
                Token = 9078
            };

            var result = await cardService.ValidateToken(model);

            Assert.True(result.Validated);

        }

        [Theory]
        [InlineData(7890)]
        [InlineData(8907)]
        [InlineData(0789)]
        public async void ValidateTokenWrongToken(int token)
        {
            TokenValidationEditModel model = new TokenValidationEditModel
            {
                CardId = 1,
                CustomerId = 1,
                CVV = 154,
                Token = token
            };

            var result = await cardService.ValidateToken(model);

            Assert.False(result.Validated);
        }

        [Fact]
        public async void TesteInvalidRegistrationDate()
        {
            TokenValidationEditModel model = new TokenValidationEditModel
            {
                CardId = 2,
                CustomerId = 1,
                CVV = 541,
                Token = 4985
            };

            var result = await cardService.ValidateToken(model);

            Assert.False(result.Validated);
        }

        private IRepository<CreditCard> GetRepository()
        {
            var cardRepository = new Mock<IRepository<CreditCard>>();
            var data = new List<CreditCard>
            {
                new CreditCard
                {
                    Id=1,
                    Number = 1234567890,
                    CVV = 154,
                    TokenRegristrationDate = DateTime.Now.AddMinutes(-10),
                    Customer = new Customer
                    {
                        Id = 1
                    }
                },
                new CreditCard
                {
                    Id=2,
                    Number = 69854,
                    CVV = 541,
                    TokenRegristrationDate = DateTime.Now.AddMinutes(-40),
                    Customer = new Customer
                    {
                        Id = 1
                    }
                }
            }.AsQueryable().BuildMock();

            cardRepository.Setup(x => x.GetAll()).Returns(data.Object);

            return cardRepository.Object;
        }
    }

    class MockLogger<T> : ILogger<CreditCard> where T : class
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {

        }
    }
}
