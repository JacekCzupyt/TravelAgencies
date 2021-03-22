//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{
    interface DatabaseInterface
    {
        DatabaseIterator GetIterator();
    }

    interface DatabaseIterator
    {
        object Current { get; }
        bool MoveNext();
        void Reset();
    }
}
