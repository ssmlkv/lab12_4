using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab10;
using ClassLibrary1;
namespace lab12_4
{
    public class MyCollection<T> : MyList<T>, IEnumerable<T> where T : IInit, ICloneable, new()
    {
        public MyCollection() : base() { }
        public MyCollection(int length) : base(length) { }
        public MyCollection(T[] collection) : base(collection) { }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void AddItems(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                AddToEnd(item);
            }
        }

        public T? FindItem(Func<T, bool> predicate)
        {
            foreach (T item in this)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return default(T);
        }

        public MyCollection<T> DeepCopy()
        {
            MyCollection<T> clonedCollection = new MyCollection<T>();

            if (beg != null)
            {
                T clonedFirstItem = (T)beg.Data.Clone();
                clonedCollection.AddToEnd(clonedFirstItem);
            }

            Point<T>? current = beg?.Next;
            while (current != null)
            {
                T clonedItem = (T)current.Data.Clone();
                clonedCollection.AddToEnd(clonedItem);
                current = current.Next;
            }

            return clonedCollection;
        }

        internal class MyEnumerator<T> : IEnumerator<T> where T : IInit, ICloneable, new()
        {
            Point<T>? beg;
            Point<T>? current;

            public MyEnumerator(MyCollection<T> collection)
            {
                beg = collection.beg;
                current = beg;
            }

            public T Current => current.Data;

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (current.Next == null)
                {
                    Reset();
                    return false;
                }
                else
                {
                    current = current.Next;
                    return true;
                }
            }

            public void Reset()
            {
                current = beg;
            }
        }
    }
}