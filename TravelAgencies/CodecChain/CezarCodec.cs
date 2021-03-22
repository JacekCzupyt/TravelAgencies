//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.CodecChains
{
    class CezarCodec : Codec
    {
        int n;
        public CezarCodec(int _n, CodecChain codec = null) : base(codec) { n = _n; }

        static int mod(int a, int m) { return ((a % m) + m) % m; }

        protected override string Decode(string value)
        {
            string tem = "";
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] < '0' || value[i] > '9')
                    throw new ArgumentException();
                tem += mod(value[i] - '0' - n, 10);
            }
            return tem;
        }

        protected override string Encode(string value)
        {
            string tem = "";
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] < '0' || value[i] > '9')
                    throw new ArgumentException();
                tem += mod(value[i] - '0' + n, 10);
            }
            return tem;
        }
    }

}
