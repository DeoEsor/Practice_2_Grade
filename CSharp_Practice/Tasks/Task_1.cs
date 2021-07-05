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
    static class Advanced_Enum<T>
         where T : IEnumerable
    { 

        /// <summary> Генерация всех возможных сочетаний из n по k
        /// 
        /// Входное перечисление: [1, 2, 3]; k == 2
        /// 
        /// Выходное перечисление: [ [1, 1], [1, 2], [1, 3], [2, 2], [2, 3], [3, 3] ]
        /// 
        /// <para>int k - порядок</para>
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Combine(T[] elems,int k) => combination(elems,k);

        /// <summary> генерация всех возможных подмножеств (без повторений)
        /// 
        /// Входное перечисление: [1, 2]
        /// 
        /// Выходное перечисление: [ [], [1], [2], [1, 2] ]
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Subset(T[] elems) => subsetting(elems);

        /// <summary> генерация всех возможных перестановок 
        /// 
        /// Входное перечисление: [1, 2, 3]
        /// 
        /// Выходное перечисление: [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1,2], [3, 2, 1] ]
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Permutations(T[] elems) => permutationing(elems);


        private static IEnumerable<IEnumerable<T>> combination(T[] elems,int k)
        {
            var ret = new List< IEnumerable < T >> ();

            for(int i=0; i< elems.Count()-1 ; i++)
            {
                T it = elems[i];

                T[] comb = new T[k];

                int iter = 0;

                for (int j=i; j< elems.Count();j++)
                {
                    
                    if (iter+1==k-1) {
                        iter = 0;
                        comb[k - 1] = it; 
                        Array.Sort(comb);
                        
                        ret.Add(GetEnum(comb));
                    }
                    comb[iter++] = elems[j];

                }

            }

            return ret;
        }

        private static IEnumerable<T> GetEnum(T[] mas) // попытка в корутины (в Unity просто время тыкал и было норм, а тут вот это)

        {
            foreach(T iter in mas)
                yield return iter;
        }

        private static IEnumerable<IEnumerable<T>> subsetting(T[] elems)
        {
            var ret = new List<IEnumerable<T>>();

            int k = elems.Count();

            for(int dim=2;dim<=k; dim++)
            for (int i = 0; i < elems.Count() - 1; i++)
            {
                T it = elems[i];

                T[] comb = new T[dim];

                int iter = 0;
                ret.Add(GetEnum(new T[] { it}));

                for (int j = i; j < elems.Count(); j++)
                {
                    if (iter + 1 == dim - 1)
                    {
                        iter = 0;
                        comb[dim - 1] = it;
                        Array.Sort(comb);
                            ret.Add(GetEnum(comb));
                        }
                    comb[iter++] = elems[j];

                }

            }

            return ret;
        }

        private static IEnumerable<IEnumerable<T>> permutationing(T[] elems)
        {
            var ret = new List<IEnumerable<T>>();

            foreach(T iter in elems)
            {

                
                
            }

            return ret;
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
            
            throw new NotImplementedException();
        }
    }
}
