﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_1lab
{
    public interface IPlayBehaviour : IObservable
    {
        void Play();
    }
}
