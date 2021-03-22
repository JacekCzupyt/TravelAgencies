//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.Agencies;

namespace TravelAgencies.Advertising
{
    class GraphicAdvertisingAgency : IAdvertising
    {
        List<ITravelAgency> Agencies;
        static int PhotoCount = 3;
        static int TemporaryOfferDisplayLimit = 2;
        Random rd;

        public GraphicAdvertisingAgency(List<ITravelAgency> l, Random _rd) { Agencies = l; rd = _rd; }


        public IOffer CreateTemporaryOffer()
        {
            ITravelAgency source = Agencies[rd.Next(Agencies.Count)];//select random travel agency to draw from
            List<IPhoto> photos = new List<IPhoto>();
            for (int i = 0; i < PhotoCount; i++)
                photos.Add(source.CreatePhoto());
            return new TemporaryGraphicOffer(source.CreateTrip(), photos, TemporaryOfferDisplayLimit);
        }

        public IOffer CreatePermamentOffer()
        {
            ITravelAgency source = Agencies[rd.Next(Agencies.Count)];//select random travel agency to draw from
            List<IPhoto> photos = new List<IPhoto>();
            for (int i = 0; i < PhotoCount; i++)
                photos.Add(source.CreatePhoto());
            return new PermamentGraphicOffer(source.CreateTrip(), photos);
        }

        public IOffer CreateOffer()
        {
            if (rd.Next(2) == 0)
                return CreatePermamentOffer();
            else
                return CreateTemporaryOffer();
        }
    }

    class PermamentGraphicOffer : PermamentOffer
    {
        ITrip trip;
        List<IPhoto> photos;

        public PermamentGraphicOffer(ITrip t, List<IPhoto> p) { trip = t; photos = p; }

        public override string OfferToString()
        {
            string ans = trip.ToString();
            foreach (IPhoto p in photos)
                ans += $"{p}\n";
            ans += "\n";
            return ans;
        }
    }

    class TemporaryGraphicOffer : TemporaryOffer
    {
        ITrip trip;
        List<IPhoto> photos;

        public TemporaryGraphicOffer(ITrip t, List<IPhoto> p, int n) : base(n) { trip = t; photos = p; }

        public override string OfferToString()
        {
            string ans = trip.ToString();
            foreach (IPhoto p in photos)
                ans += $"{p}\n";
            ans += "\n";
            return ans;
        }
    }
}
