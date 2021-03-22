//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencies.DataAccess;

namespace TravelAgencies.Agencies
{
    class FranceTravel : ITravelAgency
    {
        Random rd;

        public FranceTravel(BookingDatabase accomodationData,
                TripAdvisorDatabase tripsData,
                ShutterStockDatabase photosData,
                OysterDatabase reviewData,
                Random _rd)
        {
            accomodationDataIterator = accomodationData.GetIterator();
            tripsDataIterator = tripsData.GetIterator();
            photosDataIterator = photosData.GetIterator();
            reviewDataIterator = reviewData.GetIterator();
            rd = _rd;
        }

        DatabaseIterator accomodationDataIterator, tripsDataIterator, photosDataIterator, reviewDataIterator;

        public IPhoto CreatePhoto()
        {
            IPhoto photo;
            do
            {
                photo = new FrancePhoto(photosDataIterator.Current as PhotMetadata);
                photosDataIterator.MoveNext();
            } while (!photo.IsValid());
            return photo;
        }

        public IReview CreateReview()
        {
            IReview review = new FranceReview(reviewDataIterator.Current as BSTNode);
            reviewDataIterator.MoveNext();
            return review;
        }

        public ITrip CreateTrip()
        {
            List<TripDay> trip = new List<TripDay>();
            int NumberOfDays = rd.Next(1, 5);
            while (0 < NumberOfDays--)
            {
                List<IAttraction> attractions = new List<IAttraction>();
                for (int i = 0; i < 3; i++)
                    attractions.Add(CreateAttraction());
                trip.Add(new TripDay(CreateAccomodation(), attractions));
            }
            return new FranceTrip(trip);
        }

        private IAccommodation CreateAccomodation()
        {
            IAccommodation accomodation = new Accommodation(accomodationDataIterator.Current as ListNode);
            accomodationDataIterator.MoveNext();
            return accomodation;
        }

        private IAttraction CreateAttraction()
        {
            IAttraction attraction;
            do
            {
                var current = tripsDataIterator.Current as Tuple<TripAdvisorDatabase, Guid, int>;
                attraction = new FranceAttraction(current.Item1, current.Item2, current.Item3);
                tripsDataIterator.MoveNext();
            } while (!attraction.IsValid());
            return attraction;
        }
    }

    class FrancePhoto : AbstractPhoto
    {
        public FrancePhoto(PhotMetadata p) : base(p) { }

        static float MinLong = 0f, MaxLong = 5.4f, MinLat = 43.6f, MaxLat = 50f;

        public override bool IsValid()
        {
            return (MinLong <= Longitude && MaxLong >= Longitude && MinLat <= Latitude && MaxLat >= Latitude);
        }
    }

    class FranceTrip : AbstractTrip
    {
        public FranceTrip(List<TripDay> d) : base(d) { }
        protected override string Country { get { return "France"; } }
    }

    class FranceAttraction : AbstractAttraction
    {
        public FranceAttraction(TripAdvisorDatabase _db, Guid _id, int ni) : base(_db, _id, ni) { }

        public override bool IsValid()
        {
            return Country == "France";
        }
    }

    class FranceReview : AbstractReview
    {
        public FranceReview(BSTNode node) : base(node) { }

        public override string Review
        {
            get
            {
                string tem = base.Review;
                var split = tem.Split(' ');
                tem = "";
                for(int i=0;i<split.Length;i++)
                {
                    if (split[i].Length < 4)
                        tem += "la";
                    else
                        tem += split[i];
                    if (i != split.Length - 1)
                        tem += " ";
                }
                return tem;
            }
        }
    }
}
