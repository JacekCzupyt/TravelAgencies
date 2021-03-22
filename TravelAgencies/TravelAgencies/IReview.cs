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
    public interface IReview
    {
        string Review { get; }
        string UserName { get; }
    }

    abstract class AbstractReview : IReview
    {
        BSTNode review;

        public AbstractReview(BSTNode node) { review = node; }

        public virtual string Review { get { return review.Review; } }

        public virtual string UserName { get { return review.UserName; } }

        public override string ToString()
        {
            return $"{UserName}: {Review}";
        }
    }
}
