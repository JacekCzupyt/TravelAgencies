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
    class PolandTravel : ITravelAgency
    {
        Random rd;

        public PolandTravel(BookingDatabase accomodationData,
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
                photo = new PolandPhoto(photosDataIterator.Current as PhotMetadata);
                photosDataIterator.MoveNext();
            } while (!photo.IsValid());
            return photo;
        }

        public IReview CreateReview()
        {
            IReview review = new PolandReview(reviewDataIterator.Current as BSTNode);
            reviewDataIterator.MoveNext();
            return review;
        }

        public ITrip CreateTrip()
        {
            List<TripDay> trip = new List<TripDay>();
            int NumberOfDays = rd.Next(1, 5);
            while(0<NumberOfDays--)
            {
                List<IAttraction> attractions = new List<IAttraction>();
                for (int i = 0; i < 3; i++)
                    attractions.Add(CreateAttraction());
                trip.Add(new TripDay(CreateAccomodation(), attractions));
            }
            return new PolandTrip(trip);
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
                attraction = new PolandAttraction(current.Item1, current.Item2, current.Item3);
                tripsDataIterator.MoveNext();
            } while (!attraction.IsValid());
            return attraction;
        }
    }

    class PolandPhoto : AbstractPhoto
    {
        public PolandPhoto(PhotMetadata p) : base(p) { }

        static float MinLong = 14.4f, MaxLong = 23.5f, MinLat = 49.8f, MaxLat = 54.2f;

        public override bool IsValid()
        {
            return (MinLong <= Longitude && MaxLong >= Longitude && MinLat <= Latitude && MaxLat >= Latitude);
        }

        public override string Name
        {
            get
            {
                return base.Name.Replace('e', 'ę').Replace('a', 'ą');
            }
        }
    }

    class PolandTrip : AbstractTrip
    {
        public PolandTrip(List<TripDay> d) : base(d) { }
        protected override string Country { get { return "Poland"; } }
    }

    class PolandAttraction : AbstractAttraction
    {
        public PolandAttraction(TripAdvisorDatabase _db, Guid _id, int ni) : base(_db, _id, ni) { }

        public override bool IsValid()
        {
            return Country == "Poland";
        }
    }

    class PolandReview : AbstractReview
    {
        public PolandReview(BSTNode node) : base(node) { }

        public override string Review
        {
            get
            {
                return base.Review.Replace('e', 'ę').Replace('a', 'ą');
            }
        }

        public override string UserName
        {
            get
            {
                return base.UserName.Replace('e', 'ę').Replace('a', 'ą');
            }
        }
    }
}
