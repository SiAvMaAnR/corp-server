using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSN.Domain.Interfaces.Exception
{
    public interface IException
    {
        int Status { get; }
        string? ClientMessage { get; set; }
    }
}