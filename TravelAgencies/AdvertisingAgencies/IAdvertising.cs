//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.Advertising
{
    interface IAdvertising
    {
        IOffer CreatePermamentOffer();
        IOffer CreateTemporaryOffer();
        IOffer CreateOffer();
        //the instructions weren't clear whether advertising agencies should be able to "choose" what type of offer they create, or make a random one, so there are both options.
    }
}
