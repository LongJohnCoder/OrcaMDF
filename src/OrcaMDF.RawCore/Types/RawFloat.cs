using System;
using System.Data.SqlTypes;

namespace OrcaMDF.RawCore.Types
{
    public class RawFloat : RawType, IRawFixedLengthType
    {
        private readonly byte precision;
        private readonly byte n;

        public short Length
        {
            get
            {
                if (precision == 7)
                {
                    return Convert.ToInt16(4);
                }
                else
                {
                    return Convert.ToInt16(8);
                }
            }
        }

        public RawFloat(string name, byte n = 53) : base(name)
        {
            if (n < 1 || n > 53)
            {
                throw new NotSupportedException("Invalid parameter n in float(n)");
            }
            if (n <= 24)
            {
                this.precision = 7;
            } else if (n <= 53)
            {
                this.precision = 15;
            }
            this.n = n;
        }

        public override object GetValue(byte[] bytes)
        {
            var Double = new double();
            if (n <= 24)
            {
                Double = BitConverter.ToSingle(bytes, 0);
            } else
            {
                Double = BitConverter.ToDouble(bytes, 0);
            }
            
            return new SqlDouble(Double).Value;
        }
    }
}