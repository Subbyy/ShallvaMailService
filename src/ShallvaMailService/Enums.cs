using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallvaMailService
{
    public enum EmailActivityStatus : byte
    {
        Active,
        Sent,
        Faild,
        Canceled
    }
}
