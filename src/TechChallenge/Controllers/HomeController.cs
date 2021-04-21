﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechChallenge.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("swagger/index.html");
        }
    }
}
