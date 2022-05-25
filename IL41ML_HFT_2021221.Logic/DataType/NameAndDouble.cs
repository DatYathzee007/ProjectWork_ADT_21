using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IL41ML_HFT_2021221.Logic.DataType
{
    public class NameAndDouble : INameAndDouble
    {
        public string Name { get; set; }
        public double Average { get; set; }

        public override string ToString()
        {
            return "{ "
                + nameof(this.Name) + " = " + this.Name + " , "
                + nameof(this.Average) + " = " + Math.Round(this.Average) + " HUF"
                + " }";
        }

        public override bool Equals(object obj)
        {
            if (obj is NameAndDouble)
            {
                NameAndDouble other = obj as NameAndDouble;
                return this.Name == other.Name &&
                    this.Average == other.Average;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return 0;
        }

    }
}
