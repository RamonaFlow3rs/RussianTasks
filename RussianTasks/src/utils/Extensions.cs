using System;
using System.Collections.Generic;

namespace RussianTasks.src.utils
{
    static class Extensions
    {
        private static Random rnd = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void randomAppend<T>(this LinkedList<T> list, T value)
        {
            int size = list.Count;
            int position = rnd.Next(size);
            int method = rnd.Next(2);

            if (size > 1)
            {
                LinkedListNode<T> node = list.First;
                for (int i = 0; i < position; i++)
                {
                    node = node.Next;
                }
                if (method > 0)
                    list.AddBefore(node, value);
                else
                    list.AddAfter(node, value);
            }
            else
            {
                if (method > 0)
                    list.AddFirst(value);
                else
                    list.AddLast(value);
            }
        }
    }
}