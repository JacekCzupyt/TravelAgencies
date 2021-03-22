//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.CodecChains
{
    class SwapCodec : Codec
    {
        public SwapCodec(CodecChain codec = null) : base(codec) { }

        protected override string Decode(string value)
        {
            return Encode(value);
        }

        protected override string Encode(string value)
        {
            string ans = "";
            for (int i = 0; i + 1 < value.Length; i += 2)
            {
                ans += value[i + 1];
                ans += value[i];
            }
            if (value.Length % 2 == 1)
                ans += value[value.Length - 1];
            return ans;
        }
    }
}
