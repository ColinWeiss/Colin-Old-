﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colin.Inputs
{
    public interface Inputable
    {
        public Input InputHandle { get; set; }
    }
}