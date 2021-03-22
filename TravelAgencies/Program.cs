//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;
using TravelAgencies.Agencies;
using TravelAgencies.Advertising;

namespace TravelAgencies
{
	class Program
	{
		static void Main(string[] args) { new Program().Run(); }

		private const int WebsitePermanentOfferCount = 2;
		private const int WebsiteTemporaryOfferCount = 3;
		private Random rd = new Random(257);

		//----------
		//YOUR CODE - additional fileds/properties/methods
		//----------

		public void Run()
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			(
				BookingDatabase accomodationData, 
				TripAdvisorDatabase tripsData, 
				ShutterStockDatabase photosData, 
				OysterDatabase reviewData
			) = Init.Init.Run();


            //----------
            //YOUR CODE - set up everything

            List<ITravelAgency> TravelAgencies = new List<ITravelAgency>
            {
                new PolandTravel(accomodationData, tripsData, photosData, reviewData, rd),
                new ItalyTravel(accomodationData, tripsData, photosData, reviewData, rd),
                new FranceTravel(accomodationData, tripsData, photosData, reviewData, rd)
            };

            List<IAdvertising> AdvertisingAgencies = new List<IAdvertising>
            {
                new TextAdvertisingAgency(TravelAgencies, rd),
                new GraphicAdvertisingAgency(TravelAgencies, rd)
            };

            OfferWebsite offerWebsite = new OfferWebsite(AdvertisingAgencies, rd);
            


            //----------

            while (true)
            {
				Console.Clear();

                //----------
                //YOUR CODE - run
                offerWebsite.GetNewOffers();
				//----------

				//uncomment
				 Console.WriteLine("\n\n=======================FIRST PRESENT======================");
                 offerWebsite.Present();
                 Console.WriteLine("\n\n=======================SECOND PRESENT======================");
                 offerWebsite.Present();
                 Console.WriteLine("\n\n=======================THIRD PRESENT======================");
                 offerWebsite.Present();


                if (HandleInput()) break;
			}
		}
		bool HandleInput()
		{
			var key = Console.ReadKey(true);
			return key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Q;
		}
    }
}
