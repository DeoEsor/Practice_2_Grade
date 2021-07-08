using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

#nullable enable

namespace Tasks
{
    //TODO: ask why IEnumerable<out T> not works (or works only for interface)
    static class Advanced_Enum
    { 

        /// <summary> Генерация всех возможных сочетаний из n по k
        /// 
        /// Входное перечисление: [1, 2, 3]; k == 2
        ///     
        /// Выходное перечисление: [ [1, 1], [1, 2], [1, 3], [2, 2], [2, 3], [3, 3] ]
        /// 
        /// <para>int k - порядок</para>
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Combine<T>(this IEnumerable<T>  elems,int k) => combination<T>(elems,k);

        /// <summary> генерация всех возможных подмножеств (без повторений)
        /// 
        /// Входное перечисление: [1, 2]
        /// 
        /// Выходное перечисление: [ [], [1], [2], [1, 2] ]
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Subset<T>(this IEnumerable<T> elems) => subsetting<T>(elems);

        /// <summary> генерация всех возможных перестановок 
        /// 
        /// Входное перечисление: [1, 2, 3]
        /// 
        /// Выходное перечисление: [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1,2], [3, 2, 1] ]
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> elems) => permutationing<T>(elems);


        private static IEnumerable<IEnumerable<T>> combination<T>(this IEnumerable<T> elems,int k)
        {
            try
            {
                if (CheckEq<T>(elems)) throw new ArgumentException("In array detected equals elements");
                var ret = new List<IEnumerable<T>>();

                for (int i = 0; i < elems.Count() - 1; i++)
                {

                    T[] comb = new T[k];

                    comb[0] = elems.ElementAt(i);

                    for (int j = k - 1; j > 0; j--)
                    {
                        for (int z = i; z < elems.Count() - 1; z++)
                            comb[j] = elems.ElementAt(z);

                        ret.Add(GetEnum(comb));

                    }

                }

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return new List<IEnumerable<T>>();
            }
        }

        static bool CheckEq<T>(IEnumerable<T> mas)
        {
            Advance_Comparator<T> comp = new Advance_Comparator<T>();
            for (int i = 0; i < mas.Count() - 2; i++)
                for (int j = i; j < mas.Count() - 1; j++)
                    if(comp.Equals(mas.ElementAt(i), mas.ElementAt(j))) return true;

              return false;
        }

        private static IEnumerable<T> GetEnum<T>(this IEnumerable<T> mas) // попытка в корутины (в Unity просто время тыкал и было норм, а тут вот это)

        {
            foreach(T iter in mas)
                yield return iter;
        }

        private static IEnumerable<IEnumerable<T>> subsetting<T>(this IEnumerable<T> elems)
        {
            try
            {
                if (CheckEq<T>(elems)) throw new ArgumentException("In array detected equals elements");
                var ret = new List<IEnumerable<T>>();

                int k = elems.Count();

                for (int dim = 2; dim <= k; dim++)
                    for (int i = 0; i < elems.Count() - 1; i++)
                    {
                        T it = elems.ElementAt(i);

                        T[] comb = new T[dim];

                        int iter = 0;
                        ret.Add(GetEnum(new T[] { it }));

                        for (int j = i; j < elems.Count(); j++)
                        {
                            if ((iter + 1) % dim == 0)
                            {
                                iter = 0;
                                comb[dim - 1] = it;
                                ret.Add(GetEnum(comb));
                            }
                            comb[iter++] = elems.ElementAt(j);

                        }

                    }

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return new List<IEnumerable<T>>();
            }
        }

        private static IEnumerable<IEnumerable<T>> permutationing<T>(this IEnumerable<T> elems)
        {
            try { 
                if (CheckEq<T>(elems)) throw new ArgumentException("In array detected equals elements");
                var ret = new List<IEnumerable<T>>();
                int length = 0;
                while (elems.GetEnumerator().MoveNext())
                    length++;
                while (NextSet<T>(elems, length))
                    ret.Add(GetEnum(elems)); ;

                return ret;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return new List<IEnumerable<T>>();
            }
        }

        static bool NextSet<T>(IEnumerable<T> mas, int n)
        {
            Advance_Comparator<T> comp = new Advance_Comparator<T>();
            int j = n - 2;
            while (j != -1 ) j--;
            if (j == -1)
                return false; // больше перестановок нет
            int k = n - 1;
            while (! comp.Equals(mas.ElementAt(j),mas.ElementAt(k))) k--;k--;
            swap<T>(mas, j, k);
            int l = j + 1, r = n - 1; // сортируем оставшуюся часть последовательности
            while (l < r)
                swap<T>(mas, l++, r--);
            return true;
        }

        

        static void swap<T>(IEnumerable<T> mas, int i, int j)
        {
            T s = mas.ElementAt(i);
            T h = mas.ElementAt(i);
            h=mas.ElementAt(j);
            var g = mas.ElementAt(j);
            g = s;
        }

    }

    class Advance_Comparator<T> : IEqualityComparer<T>
    {
        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            if (x.Equals(y)) return true;
            else return false;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
