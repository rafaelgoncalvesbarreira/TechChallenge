﻿using Microsoft.AspNetCore.Http;
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
    public class CardController : ControllerBase
    {
        private readonly ICardService service;
        public CardController(ICardService cardService)
        {
            service = cardService;
        }

        [HttpPost]
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
