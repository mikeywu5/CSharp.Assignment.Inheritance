﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc
{
    public class BaseCalc
    {
        protected BaseCalc()
        {
        }
        public virtual int Calculate()
        {
            throw new NotImplementedException();
        }
    }
}