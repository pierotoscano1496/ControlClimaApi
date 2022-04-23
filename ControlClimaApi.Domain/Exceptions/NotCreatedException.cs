using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlClimaApi.Domain.Exceptions
{
    public class NotCreatedException : Exception
    {
        public NotCreatedException(string message) : base(message)
        {

        }
    }
}
