//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.Advertising
{
    public interface IOffer
    {
        string OfferToString();
    }

    public abstract class TemporaryOffer : IOffer
    {
        int RemainingDisplayes;

        public abstract string OfferToString();

        public TemporaryOffer(int n) { RemainingDisplayes = n; }

        public override string ToString()
        {
            if (0 < RemainingDisplayes--)
                return OfferToString();
            else
                return "This offer has expired.\n\n";
        }
    }

    public abstract class PermamentOffer : IOffer
    {
        public abstract string OfferToString();

        public override string ToString()
        {
            return OfferToString();
        }
    }

}
