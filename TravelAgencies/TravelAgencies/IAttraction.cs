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
    public interface IAttraction
    {
        string Name { get; }
        string Price { get; }
        string Rating { get; }
        string Country { get; }
        bool IsValid();
    }

    abstract class AbstractAttraction : IAttraction
    {
        TripAdvisorDatabase db;
        Guid id;
        int NameIndex;

        public AbstractAttraction(TripAdvisorDatabase _db, Guid _id, int ni) { db = _db; id = _id; NameIndex = ni; }

        static CodecChain AttractionCodec = new CodecChainStarter(new PushCodec(3, new FrameCodec(2, new SwapCodec(new PushCodec(3)))));

        public string Name { get { return db.Names[NameIndex][id]; } }
        public string Price { get { return AttractionCodec.Decrypt(db.Prices[id]); } }
        public string Rating { get { return AttractionCodec.Decrypt(db.Ratings[id]); } }
        public string Country { get { return db.Countries[id]; } }

        public abstract bool IsValid();

        public override string ToString()
        {
            return Name;
        }
    }
}
