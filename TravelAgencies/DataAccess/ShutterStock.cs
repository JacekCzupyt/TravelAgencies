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
	class PhotMetadata
	{
		public string Name { get; set; }
		public string Camera { get; set; }
		public double[] CameraSettings { get; set; }
		public DateTime Date { get; set; }
		public string WidthPx { get; set; }//Encrypted
		public string HeightPx { get; set; }//Encrypted
		public double Longitude { get; set; }
		public double Latitude { get; set; }
	}
	class ShutterStockDatabase : DatabaseInterface
	{
		public PhotMetadata[][][] Photos;

        public DatabaseIterator GetIterator()
        {
            return new ShutterStockDatabaseIterator(this);
        }
    }

    class ShutterStockDatabaseIterator : DatabaseIterator
    {
        ShutterStockDatabase database;
        int i = 0, j = 0, k = 0;

        public ShutterStockDatabaseIterator(ShutterStockDatabase d)
        {
            database = d;
            if (!IsValid())
                MoveNext();
        }

        public object Current
        {
            get
            {
                return database.Photos[i][j][k];
            }
        }

        private bool IsValid()
        {
            if (database.Photos.Length > i &&
                database.Photos[i] != null &&
                database.Photos[i].Length > j &&
                database.Photos[i][j] != null &&
                database.Photos[i][j].Length > k &&
                database.Photos[i][j][k] != null)
                return true;
            else
                return false;
        }


        public bool MoveNext()
        {
            do { k++; } while (database.Photos[i][j].Length > k && database.Photos[i][j][k] == null);
            if(database.Photos[i][j].Length <= k)
            {
                k = 0;
                do { j++; } while (database.Photos[i].Length > j && database.Photos[i][j] == null);
                if(database.Photos[i].Length <= j)
                {
                    j = 0;
                    do { i++; } while (database.Photos.Length > i && database.Photos[i] == null);
                    if(database.Photos.Length <= i)
                    {
                        Reset();
                    }
                }
            }
            if (!IsValid())
                MoveNext();
            return true;
        }

        public void Reset()
        {
            i = 0;
            j = 0;
            k = 0;
        }
    }
}
