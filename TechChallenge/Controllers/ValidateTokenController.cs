using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechChallenge.Application.Contract;
using TechChallenge.Application.ViewModel;

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
        public async Task<IActionResult> Post(TokenValidationModel model)
        {
            var isValid = await service.ValidateToken(model);
            return Ok(isValid);
        }
    }
}
