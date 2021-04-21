using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.ViewModels;
using TechChallenge.Data.Contracts;
using TechChallenge.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TechChallenge.Application.Services
{
    public class CardServices : ICardService
    {
        private readonly IRepository<CreditCard> repository;
        private readonly ILogger<CreditCard> _logger;
        public CardServices(IRepository<CreditCard> repositoryCreditCard, ILogger<CreditCard> logger)
        {
            repository = repositoryCreditCard;
            _logger = logger;
        }
        public async Task<CardViewModel> CreateAsync(CardEditModel model)
        {
            var creditCard = new CreditCard
            {
                Customer = new Customer { Id = model.CustomerId },
                Number = model.CardNumber,
                CVV = model.CVV,
                TokenRegristrationDate = DateTime.Now
            };
            var token = GenerateToken(model.CardNumber, model.CVV);

            creditCard = await repository.InsertAsync(creditCard);

            return new CardViewModel
            {
                CardId = creditCard.Id,
                RegistrationDate = creditCard.TokenRegristrationDate,
                Token = token
            };
        }

        public async Task<TokenValidationViewModel> ValidateToken(TokenValidationEditModel model)
        {
            var tokenValidation = new TokenValidationViewModel();

            var entity = await repository
                .GetAll()
                .FirstOrDefaultAsync(o =>
                    o.Customer.Id == model.CustomerId
                    && o.Id == model.CardId);

            if (entity == null)
            {
                return tokenValidation;
            }

            var token = GenerateToken(entity.Number, entity.CVV);
            
            tokenValidation.Validated = (entity.TokenRegristrationDate.AddMinutes(30) >= DateTime.Now)
                && (token == model.Token);
            _logger.LogInformation($"Card Number: {entity.Number} Valid: {tokenValidation.Validated}");

            return tokenValidation;
        }

        public long GenerateToken(long cardNumber, int cvv)
        {
            var seed = GetLast4digits(cardNumber);

            var k_afterManyIteration = cvv % seed.Count;

            var splitPoint = seed.Count - k_afterManyIteration;
            var firstHalf = seed.GetRange(0, splitPoint);
            var secondHalf = seed.GetRange(splitPoint, seed.Count - splitPoint);

            var finalList = new List<int>();
            finalList.AddRange(secondHalf);
            finalList.AddRange(firstHalf);

            return ListToLong(finalList);
        }

        private List<int> GetLast4digits(long number)
        {
            var last4digits = (int)(number % 10000);
            var list = new List<int>();

            var i = last4digits;
            while(i != 0)
            {
                var digit = i % 10;
                list.Insert(0, digit);
                i = i / 10;
            }

            return list;
        }

        private long ListToLong(List<int> list)
        {
            var index = list.Count - 1;
            var multiplier = 1;
            var result = 0;
            while(index >= 0)
            {
                result += list[index] * multiplier;
                multiplier = multiplier * 10;
                index--;
            }

            return result;
        }
    }
}
