//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.CodecChains
{
    class PushCodec : Codec
    {
        int n;
        public PushCodec(int _n, CodecChain codec = null) : base(codec) { n = _n; }

        protected override string Decode(string value)
        {
            if (value.Length == 0)
                throw new ArgumentException();
            int nm = ((n % value.Length) + value.Length) % value.Length;//why does the % operator return negative numbers?? >:|
            return value.Substring(nm, value.Length - nm) + value.Substring(0, nm);
        }

        protected override string Encode(string value)
        {
            if (value.Length == 0)
                throw new ArgumentException();
            int nm = value.Length - (((n % value.Length) + value.Length) % value.Length);
            return value.Substring(nm, value.Length - nm) + value.Substring(0, nm);
        }
    }

}
