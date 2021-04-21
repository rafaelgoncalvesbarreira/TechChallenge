using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Application.ViewModels;

namespace TechChallenge.Application.Contracts
{
    public interface ICardService
    {
        Task<CardViewModel> CreateAsync(CardEditModel model);
        Task<TokenValidationViewModel> ValidateToken(TokenValidationEditModel model);
    }
}
