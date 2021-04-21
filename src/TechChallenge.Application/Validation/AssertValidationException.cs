using System;
using System.Collections.Generic;
using System.Text;

namespace TechChallenge.Application.Validation
{
    public class AssertValidationException: Exception
    {
        public AssertValidationException(string message): base(message)
        {

        }
    }
}
