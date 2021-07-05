﻿using System;
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
        public static IEnumerable<IEnumerable<T>> Combine<T>(this T[] elems,int k) => combination<T>(elems,k);

        /// <summary> генерация всех возможных подмножеств (без повторений)
        /// 
        /// Входное перечисление: [1, 2]
        /// 
        /// Выходное перечисление: [ [], [1], [2], [1, 2] ]
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Subset<T>(this T[] elems) => subsetting<T>(elems);

        /// <summary> генерация всех возможных перестановок 
        /// 
        /// Входное перечисление: [1, 2, 3]
        /// 
        /// Выходное перечисление: [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1,2], [3, 2, 1] ]
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this T[] elems) => permutationing<T>(elems);


        private static IEnumerable<IEnumerable<T>> combination<T>(this T[] elems,int k)
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

        private static IEnumerable<T> GetEnum<T>(this T[] mas) // попытка в корутины (в Unity просто время тыкал и было норм, а тут вот это)

        {
            foreach(T iter in mas)
                yield return iter;
        }

        private static IEnumerable<IEnumerable<T>> subsetting<T>(this T[] elems)
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

        private static IEnumerable<IEnumerable<T>> permutationing<T>(this T[] elems)
        {
            var ret = new List<IEnumerable<T>>();

            while (NextSet<T>(ref elems, elems.Length))
                ret.Add(GetEnum(elems)); ;

            return ret;
        }

        static bool NextSet<T>(ref T[] mas, int n)
        {
            int j = n - 2;
            while (j != -1 ) j--;
            if (j == -1)
                return false; // больше перестановок нет
            int k = n - 1;
            while (!Advance_Comparator.Equals(mas[j],mas[k])) k--;k--;
            swap<T>(ref mas, j, k);
            int l = j + 1, r = n - 1; // сортируем оставшуюся часть последовательности
            while (l < r)
                swap<T>(ref mas, l++, r--);
            return true;
        }

        

        static void swap<T>(ref T[] mas, int i, int j)
        {
            T s = mas[i];
            mas[i] = mas[j];
            mas[j] = s;
        }

    }

    static class Advance_Comparator
    {
        static bool Equals<T>([AllowNull] T x, [AllowNull] T y)
        {
            if (x.Equals(y)) return true;
            else return false;
        }
    }
}