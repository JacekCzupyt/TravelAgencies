//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{
	class TripAdvisorDatabase : DatabaseInterface
	{
		public Guid[] Ids;
		public Dictionary<Guid, string>[] Names { get; set; }
		public Dictionary<Guid, string> Prices { get; set; }//Encrypted
		public Dictionary<Guid, string> Ratings { get; set; }//Encrypted
		public Dictionary<Guid, string> Countries { get; set; }

        public DatabaseIterator GetIterator()
        {
            return new TripAdvisorDatabaseIterator(this);
        }
    }

    class TripAdvisorDatabaseIterator : DatabaseIterator
    {
        TripAdvisorDatabase database;
        int i;

        public TripAdvisorDatabaseIterator(TripAdvisorDatabase d)
        {
            database = d;
            if (!IsValid())
                MoveNext();
        }

        public object Current
        {
            get
            {
                Guid id = database.Ids[i];
                string name = null, price = null, rating = null, country = null;
                int k;
                for(k=0;k<database.Names.Length;k++)
                {
                    if (database.Names[k].TryGetValue(id, out name))
                        break;
                }

                database.Prices.TryGetValue(id, out price);
                database.Ratings.TryGetValue(id, out rating);
                database.Countries.TryGetValue(id, out country);

                if (name != null && name != "" &&
                    price != null && price != "" &&
                    rating != null && rating != "" &&
                    country != null && country != "")
                    return new Tuple<TripAdvisorDatabase, Guid, int>(database, id, k);
                else
                    throw new IndexOutOfRangeException();
            }
        }

        bool IsValid()
        {
            if (database.Ids.Length <= i)
                return false;
            Guid id = database.Ids[i];
            string name = null, price = null, rating = null, country = null;
            foreach (var map in database.Names)
            {
                if (map.TryGetValue(id, out name))
                    break;
            }
            database.Prices.TryGetValue(id, out price);
            database.Ratings.TryGetValue(id, out rating);
            database.Countries.TryGetValue(id, out country);

            if (name != null && name != "" &&
                price != null && price != "" &&
                rating != null && rating != "" &&
                country != null && country != "")
                return true;
            else
                return false;
        }

        public bool MoveNext()
        {
            i++;
            if (i > database.Ids.Length)
                Reset();
            if (!IsValid())
                MoveNext();
            return true;
        }

        public void Reset()
        {
            i = 0;
            if (!IsValid())
                MoveNext();
        }
    }
}

