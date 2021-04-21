using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Application.ViewModel;

namespace TechChallenge.Application.Contract
{
    public interface ICardService
    {
        Task<CardViewModel> CreateAsync(CardEditModel model);
        Task<bool> ValidateToken(TokenValidationModel model);
    }
}
