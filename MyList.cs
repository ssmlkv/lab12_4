using System;
using ClassLibrary1;
using lab10;
using lab12_4;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab12_4
{
    public class MyList<T> where T : IInit, ICloneable, new()
    {
        public Point<T>? beg = null;
        public Point<T>? end = null;

        int count = 0;

        public int Count => count;

        public Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit();
            return new Point<T>(data);
        }

        public T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit();
            return data;
        }

        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (beg != null)
            {
                beg.Prev = newItem;
                newItem.Next = beg;
                beg = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (end != null)
            {
                end.Next = newItem;
                newItem.Prev = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        public MyList() { }

        public MyList(int size)
        {
            if (size <= 0) throw new Exception("size меньше нуля");
            beg = MakeRandomData();
            end = beg;
            for (int i = 1; i < size; i++)
            {
                T newItem = MakeRandomItem();
                AddToEnd(newItem);
            }
            count = size;
        }

        public MyList(T[] collection)
        {
            if (collection == null)
                throw new Exception("empty collection: null");
            if (collection.Length == 0)
                throw new Exception("empty collection");
            T newData = (T)collection[0].Clone();
            beg = new Point<T>(newData);
            end = beg;
            for (int i = 1; i < collection.Length; i++)
            {
                AddToEnd(collection[i]);
            }
        }

        public void PrintList()
        {
            if (count == 0)
                Console.WriteLine("Пусто");
            Point<T>? current = beg;
            for (int i = 0; current != null; i++)
            {
                Console.WriteLine(current);
                current = current.Next;
            }
        }

        public Point<T>? FindItem(T item)
        {
            Point<T>? current = beg;
            while (current != null)
            {
                if (current.Data == null)
                    throw new Exception("Data is null");
                if (current.Data.Equals(item))
                    return current;
                current = current.Next;
            }
            return null;
        }

        public bool RemoveItem(T item)
        {
            if (beg == null)
            {
                throw new Exception("Список пуст");
            }

            Point<T>? pos = FindItem(item);
            if (pos == null)
            {
                return false;
            }

            count--;

            if (beg == end)
            {
                beg = end = null;
                return true;
            }

            if (pos == beg)
            {
                beg = beg.Next;
                if (beg != null)
                {
                    beg.Prev = null;
                }
                else
                {
                    end = null;
                }
                return true;
            }

            if (pos == end)
            {
                end = end.Prev;
                if (end != null)
                {
                    end.Next = null;
                }
                else
                {
                    beg = null;
                }
                return true;
            }

            Point<T> next = pos.Next;
            Point<T> prev = pos.Prev;
            pos.Next.Prev = prev;
            pos.Prev.Next = next;
            return true;
        }

        public MyList<T> Clone()
        {
            MyList<T> newList = new MyList<T>();

            Point<T>? current = beg;
            while (current != null)
            {
                T newData = (T)current.Data.Clone();
                newList.AddToEnd(newData);
                current = current.Next;
            }

            return newList;
        }

        public void Clear()
        {
            beg = null;
            end = null;
            count = 0;
        }
    }
}