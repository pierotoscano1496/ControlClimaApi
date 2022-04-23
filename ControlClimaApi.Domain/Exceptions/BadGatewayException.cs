using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Exceptions
{
    public class BadGatewayException : Exception
    {
        public BadGatewayException(string message) : base(message)
        {

        }
    }
}
