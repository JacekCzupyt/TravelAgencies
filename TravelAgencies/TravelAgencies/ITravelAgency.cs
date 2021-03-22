//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.Agencies
{
    public interface ITravelAgency
    {
        ITrip CreateTrip();
        IPhoto CreatePhoto();
        IReview CreateReview();
    }   
}