using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Application.Contract;
using TechChallenge.Application.ViewModel;
using TechChallenge.Data.Contract;
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
                RegristrationDate = DateTime.Now
            };
            creditCard.Token = generateToken(model.CardNumber, model.CVV);

            creditCard = await repository.InsertAsync(creditCard);

            return new CardViewModel
            {
                CardId = creditCard.Id,
                RegistrationDate = creditCard.RegristrationDate,
                Token = creditCard.Token
            };
        }

        public async Task<bool> ValidateToken(TokenValidationModel model)
        {
            var entity = await repository
                .GetAll()
                .FirstOrDefaultAsync(o =>
                    o.Customer.Id == model.CustomerId
                    && o.Id == model.CardId
                    && o.Token == model.Token);

            if (entity == null)
            {
                return false;
            }


            var isValid = entity.RegristrationDate.AddMinutes(30) >= DateTime.Now;
            _logger.LogInformation($"Card Number: {entity.Number.ToString()}. Valid: {isValid}");

            return isValid;
        }

        private long generateToken(long cardNumber, int cvv)
        {
            var seed = GetLast4digits(cardNumber);

            var k_afterManyIteration = cvv % seed.Count;

            var splitPoint = seed.Count - k_afterManyIteration;
            var firstHalf = seed.GetRange(0, splitPoint);
            var secondHalf = seed.GetRange(splitPoint, seed.Count - splitPoint);

            var finalList = new List<int>();
            finalList.AddRange(firstHalf);
            finalList.AddRange(secondHalf);

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
