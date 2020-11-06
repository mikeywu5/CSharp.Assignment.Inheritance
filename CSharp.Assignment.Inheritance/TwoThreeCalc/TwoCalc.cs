using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc
{
    public class TwoCalc : BaseCalc
    {
        private readonly int _a;
        private readonly int _b;
        public TwoCalc(int a, int b)
        {
            _a = a;
            _b = b;
        }
        public override int Calculate()
        {
            return _a + 2 * _b;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", _a, _b);
        }
    }
}
