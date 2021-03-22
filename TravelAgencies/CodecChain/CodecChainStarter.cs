//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.CodecChains
{
    class CodecChainStarter : Codec // this codec re-encrypts the decrypted data to check its validity
    {
        public CodecChainStarter(CodecChain codec = null) : base(codec) { }

        public override string Decrypt(string value)
        {
            string decrypted;
            if (NextInChain == null)
                decrypted = value;
            else
                decrypted = NextInChain.Decrypt(value);

            if (Encrypt(decrypted) == value)
                return decrypted;
            else
            {
                Encrypt(decrypted);
                throw new ArgumentException();
            }

        }

        protected override string Decode(string value)
        {
            return value;
        }

        protected override string Encode(string value)
        {
            return value;
        }
    }
}
