using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Assignment.Inheritance.TwoThreeCalc
{
    public class ListCalc : BaseCalc
    {
        protected readonly List<BaseCalc> Calculations;

        public ListCalc(List<BaseCalc> calculations)
        {
            if (calculations == null) throw new ArgumentNullException("calculations");
            Calculations = calculations;
        }

        public ListCalc(params int[] values)
        {
            if (values == null) throw new ArgumentNullException("values");
            if (values.Length < 2) throw new ArgumentException();
            int length = values.Length;
            int mod = length % 3;
            Calculations = new List<BaseCalc>();
            int three = length / 3;
            int start = 0;
            if (mod >= 1)
            {
                start += 2;
                Calculations.Add(new TwoCalc(values[0], values[1]));
                if (mod == 1) three--;
            }

            for (int i = start; i < three * 3; i += 3)
            {
                Calculations.Add(new ThreeCalc(values[i], values[i + 1], values[i + 2]));
            }

            if (mod == 1)
            {
                Calculations.Add(new TwoCalc(values[length - 2], values[length - 1]));
            }
        }
        public override int Calculate()
        {
            var sum = 0;
            for (int i = 0; i < Calculations.Count; i++)
            {
                sum += Calculations[i].Calculate();
            }
            return sum;
        }

        public override string ToString()
        {
            var x = "";
            for (int i = 0; i < Calculations.Count; i++)
            {
                if (x != "")
                {
                    x += "; ";
                }
                x += Calculations[i].ToString();
            }
            return x;
        }
    }
}
