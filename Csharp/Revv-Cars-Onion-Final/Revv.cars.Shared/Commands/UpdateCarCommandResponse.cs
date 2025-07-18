using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revv.cars.Shared.Commands
{
    public class UpdateCarCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
    }
}
