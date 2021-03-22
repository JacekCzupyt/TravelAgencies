//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.Agencies
{
    public interface ITrip
    {
        List<TripDay> Days { get; }
    }

    public abstract class AbstractTrip : ITrip
    {
        protected abstract string Country { get; }
        public AbstractTrip(List<TripDay> d) { Days = d; }
        public List<TripDay> Days { get; private set; }
        public override string ToString()
        {
            string ans = $"Rating: {Rating}\n" +
                $"Price: {Price}\n\n";

            for (int i=0;i<Days.Count;i++)
            {
                ans += $"Day {i+1} in {Country}\n" +
                    $"{Days[i]}\n";
            }

            return ans;
        }
        int Price { get { return Days.Sum(item => item.Price); } }
        float Rating { get { return Days.Average(item => item.Rating); } }
    }

    public struct TripDay
    {
        public IAccommodation accomodation;
        public List<IAttraction> attractions;

        public TripDay(IAccommodation a, List<IAttraction> b) { accomodation = a; attractions = b; }

        public override string ToString()
        {
            string ans = $"Accommodation: {accomodation}\nAttractions:\n";
            foreach (IAttraction att in attractions)
                ans += $"        {att}\n";
            return ans;
        }

        public int Price
        {
            get
            {
                int p = Int32.Parse(accomodation.Price);
                foreach (IAttraction att in attractions)
                    p += Int32.Parse(att.Price);
                return p;
            }
        }

        public float Rating
        {
            get
            {
                float p = float.Parse(accomodation.Rating);
                foreach (IAttraction att in attractions)
                    p += float.Parse(att.Rating);
                return p / (attractions.Count + 1);
            }
        }
    }
}
