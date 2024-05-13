using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiplanDotnetAPI.Application.Exceptions;
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        
    }
}
