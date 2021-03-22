//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencies.DataAccess
{

    class ListNode
    {
        public ListNode Next { get; set; }
        public string Name { get; set; }
        public string Rating { get; set; }//Encrypted
        public string Price { get; set; }//Encrypted

        public ListNode this[int key]
        {
            get
            {
                ListNode current = this;
                for(int i=0;i<key;i++)
                {
                    current = current.Next;
                    if (current == null)
                        return null;
                }
                return current;
            }
        }
	}
   

	class BookingDatabase : DatabaseInterface
	{
		public ListNode[] Rooms { get; set; }

        public DatabaseIterator GetIterator()
        {
            return new BookingDatabaseIterator(this);
        }
    }

    class BookingDatabaseIterator : DatabaseIterator
    {
        BookingDatabase database;
        int i = 0;
        int j = 0;

        public BookingDatabaseIterator(BookingDatabase d) { database = d; }

        public object Current
        {
            get
            {
                return database.Rooms[i][j];
            }
        }

        public bool MoveNext()
        {
            int j0 = j;
            do
            {
                i++;
                if (i >= database.Rooms.Length)
                {
                    i = 0;
                    j++;
                }
            } while (j0 + 2 > j && Current == null);
            if (Current == null)
            {
                Reset();
                if (Current == null)
                    MoveNext();//in case the rooms[0] = null
            }
            return true;
        }

        public void Reset()
        {
            i = 0;
            j = 0;
        }
    }
}
