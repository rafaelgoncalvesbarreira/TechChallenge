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
    public class CardController : ControllerBase
    {
        private readonly ICardService service;
        public CardController(ICardService cardService)
        {
            service = cardService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CardViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CardEditModel model)
        {
            if (model.isValid())
            {
                var result = await service.CreateAsync(model);
                return Ok(result);
            }

            return ValidationProblem();
        }
    }
}
