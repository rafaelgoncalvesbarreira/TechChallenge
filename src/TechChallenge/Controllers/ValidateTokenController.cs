using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechChallenge.Application.Contracts;
using TechChallenge.Application.ViewModels;

namespace TechChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateTokenController : ControllerBase
    {
        private readonly ICardService service;
        public ValidateTokenController(ICardService cardService)
        {
            service = cardService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TokenValidationViewModel), (int)HttpStatusCode.OK)]
        public async Task<TokenValidationViewModel> Post(TokenValidationEditModel model)
        {
            return await service.ValidateToken(model);
        }
    }
}
