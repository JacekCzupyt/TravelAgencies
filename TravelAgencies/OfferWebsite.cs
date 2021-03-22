//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.Advertising;

namespace TravelAgencies
{
    class OfferWebsite
    {
        List<IAdvertising> AdvertisingAgencies;
        static int WebsitePermamentOfferCount = 2;
        static int WebsiteTemporaryOfferCount = 2;
        List<IOffer> Offers = new List<IOffer>();

        Random rd;

        public OfferWebsite(List<IAdvertising> l, Random _rd) { AdvertisingAgencies = l; rd = _rd; }

        public void GetNewOffers()
        {
            Offers.Clear();
            for(int i=0;i<WebsitePermamentOfferCount;i++)
            {
                Offers.Add(AdvertisingAgencies[rd.Next(AdvertisingAgencies.Count)].CreatePermamentOffer());
            }
            for (int i = 0; i < WebsiteTemporaryOfferCount; i++)
            {
                Offers.Add(AdvertisingAgencies[rd.Next(AdvertisingAgencies.Count)].CreateTemporaryOffer());
            }
        }

        public void Present()
        {
            if (Offers.Count == 0)
                throw new Exception();
            foreach(IOffer offer in Offers)
            {
                Console.Write(offer);
            }
        }
    }
}
