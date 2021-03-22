//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.CodecChains
{
    class FrameCodec : Codec
    {
        int n;
        public FrameCodec(int _n, CodecChain codec = null) : base(codec)
        {
            if (_n >= 0 && _n <= 9)
                n = _n;
            else
                throw new ArgumentException();
        }

        protected override string Encode(string value)
        {
            string wrap = "";
            for (int i = 1; i <= n; i++)
                wrap += $"{i}";
            return wrap + value + new string(wrap.Reverse().ToArray());

        }

        protected override string Decode(string value)
        {
            if (n == 0)
                return value;
            string wrap = Encode("");


            if (value.Length >= 2 * n &&
                value.Substring(0, n) == wrap.Substring(0, n) &&
                value.Substring(value.Length - n, n) == wrap.Substring(n, n))
            {
                return value.Substring(n, value.Length - (2 * n));
            }
            else
                throw new ArgumentException();
        }
    }
}
