using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc
{
    public class ThreeCalc : TwoCalc
    {
        private readonly int _c;

        public ThreeCalc(int a, int b, int c)
            : base(a, b)
        {
            _c = c;
        }

        public override int Calculate()
        {
            return base.Calculate() + 3 * _c;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", base.ToString(), _c);
        }
    }
}
