//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;
using TravelAgencies.CodecChains;

namespace TravelAgencies.Agencies
{
    public interface IAccommodation
    {
        string Name { get; }
        string Rating { get; }
        string Price { get; }
    }

    class Accommodation : IAccommodation
    {
        ListNode node;

        static CodecChain AccommodationCodec = new CodecChainStarter(new FrameCodec(2, new ReverseCodec(new CezarCodec(-1, new SwapCodec()))));

        public Accommodation(ListNode n) { node = n; }

        public string Name { get { return node.Name; } }
        public string Rating { get { return AccommodationCodec.Decrypt(node.Rating); } }
        public string Price { get { return AccommodationCodec.Decrypt(node.Price); } }

        public override string ToString()
        {
            return Name;
        }
    }
}
