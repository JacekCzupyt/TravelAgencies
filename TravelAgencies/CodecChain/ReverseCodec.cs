//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.CodecChains
{
    class ReverseCodec : Codec
    {
        public ReverseCodec(CodecChain codec = null) : base(codec) { }

        protected override string Encode(string value)
        {
            return new string(value.Reverse().ToArray());
        }

        protected override string Decode(string value)
        {
            return Encode(value);
        }

    }

}
