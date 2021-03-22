//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.CodecChains
{
    interface CodecChain
    {
        void SetNextInChain(CodecChain codec);
        string Encrypt(string value);
        string Decrypt(string value);
    }

    abstract class Codec : CodecChain
    {
        protected CodecChain NextInChain;

        public Codec(CodecChain codec = null) { SetNextInChain(codec); }

        public virtual string Decrypt(string value)
        {
            if (NextInChain == null)
                return Decode(value);
            else
                return Decode(NextInChain.Decrypt(value));
        }

        public string Encrypt(string value)
        {
            //Console.WriteLine($"{this} {value} => {Encode(value)}");
            if (NextInChain == null)
                return Encode(value);
            else
                return NextInChain.Encrypt(Encode(value));
        }

        public void SetNextInChain(CodecChain codec)
        {
            if (NextInChain == null)
                NextInChain = codec;
            else
                NextInChain.SetNextInChain(codec);
        }

        protected abstract string Encode(string value);
        protected abstract string Decode(string value);
    }

}
