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
    class TextAdvertisingAgency : IAdvertising
    {
        List<ITravelAgency> Agencies;
        static int ReviewCount = 3;
        static int TemporaryOfferDisplayLimit = 2;
        Random rd;

        public TextAdvertisingAgency(List<ITravelAgency> l, Random _rd) { Agencies = l; rd = _rd; }


        public IOffer CreateTemporaryOffer()
        {
            ITravelAgency source = Agencies[rd.Next(Agencies.Count)];//select random travel agency to draw from
            List<IReview> reviews = new List<IReview>();
            for (int i = 0; i < ReviewCount; i++)
                reviews.Add(source.CreateReview());
            return new TemporaryTextOffer(source.CreateTrip(), reviews, TemporaryOfferDisplayLimit);
        }

        public IOffer CreatePermamentOffer()
        {
            ITravelAgency source = Agencies[rd.Next(Agencies.Count)];//select random travel agency to draw from
            List<IReview> reviews = new List<IReview>();
            for (int i = 0; i < ReviewCount; i++)
                reviews.Add(source.CreateReview());
            return new PermamentTextOffer(source.CreateTrip(), reviews);
        }

        public IOffer CreateOffer()
        {
            if (rd.Next(2) == 0)
                return CreatePermamentOffer();
            else
                return CreateTemporaryOffer();
        }
    }

    class PermamentTextOffer : PermamentOffer
    {
        ITrip trip;
        List<IReview> reviews;

        public PermamentTextOffer(ITrip t, List<IReview> r) { trip = t; reviews = r; }

        public override string OfferToString()
        {
            string ans = trip.ToString();
            foreach (IReview r in reviews)
                ans += $"{r}\n";
            ans += "\n";
            return ans;
        }
    }

    class TemporaryTextOffer : TemporaryOffer
    {
        ITrip trip;
        List<IReview> reviews;

        public TemporaryTextOffer(ITrip t, List<IReview> r, int n):base(n) { trip = t; reviews = r; }

        public override string OfferToString()
        {
            string ans = trip.ToString();
            foreach (IReview r in reviews)
                ans += $"{r}\n";
            ans += "\n";
            return ans;
        }
    }
}
