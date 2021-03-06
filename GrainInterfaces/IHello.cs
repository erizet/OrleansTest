﻿using System;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
        Task<int> GetCounter();

    }
}
