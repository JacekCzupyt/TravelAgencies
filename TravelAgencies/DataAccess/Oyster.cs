//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  Jacek Czupyt


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Oyster is a website with reviews of various holiday destinations.
namespace TravelAgencies.DataAccess
{

	class BSTNode : DatabaseInterface
    {
		public string Review { get; set; }
		public string UserName { get; set; }
		public BSTNode Left { get; set; }
		public BSTNode Right { get; set; }


        public DatabaseIterator GetIterator()
        {
            return new OysterTreeIterator(this);
        }
    }

	class OysterDatabase : DatabaseInterface
	{
		public BSTNode Reviews { get; set; }

        public DatabaseIterator GetIterator()
        {
            return new OysterDatabaseIterator(this);
        }
    }

    class OysterDatabaseIterator : DatabaseIterator
    {
        DatabaseIterator it;

        public OysterDatabaseIterator(OysterDatabase d) { it = d.Reviews.GetIterator(); }

        public object Current
        {
            get
            {
                return it.Current;
            }
        }

        public bool MoveNext()
        {
            if (!it.MoveNext())
                it.Reset();
            return true;
        }

        public void Reset()
        {
            it.Reset();
        }
    }

    class OysterTreeIterator : DatabaseIterator
    {
        BSTNode node;
        DatabaseIterator it = null;
        bool left = true;

        public OysterTreeIterator(BSTNode n)
        {
            node = n;
            if (node.Left != null)
            {
                it = node.Left.GetIterator();
            }
            else
                left = false;
        }

        public object Current
        { get
            {
                if (it == null)
                    return node;
                else
                    return it.Current;
            } }

        public bool MoveNext()
        {
            if(it!=null)
            {
                if(!it.MoveNext())
                {
                    if (left)
                        it = null;
                    else
                        return false;
                }
                return true;
            }
            else
            {
                if (node.Right != null)
                {
                    it = node.Right.GetIterator();
                    left = false;
                    return true;
                }
                else
                    return false;
            }
        }

        public void Reset()
        {
            if (node.Left != null)
            {
                it = node.Left.GetIterator();
                left = true;
            }
            else
            {
                it = null;
                left = false;
            }
                
        }
    }
}
