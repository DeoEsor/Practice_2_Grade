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
    class Advanced_Enum<T> : IEnumerable<T>
    {
        public Advanced_Enum(T[] elems)
        {
            _elems = elems.OfType<T>().ToList();
        }

        public Advanced_Enum(Advanced_Enum<T> right)
        {
            _elems = right._elems.OfType<T>().ToList();
        }

        public List<T> _elems = new List<T>();
        public IEnumerator<T> Elements => _elems.GetEnumerator();

        /// <summary> Генерация всех возможных сочетаний из n по k
        /// 
        /// Входное перечисление: [1, 2, 3]; k == 2
        /// 
        /// Выходное перечисление: [ [1, 1], [1, 2], [1, 3], [2, 2], [2, 3], [3, 3] ]
        /// 
        /// <para>int k - порядок</para>
        /// </summary>
        public IEnumerator<IEnumerator<T>> Combine(int k) => combination(k);

        /// <summary> генерация всех возможных подмножеств (без повторений)
        /// 
        /// Входное перечисление: [1, 2]
        /// 
        /// Выходное перечисление: [ [], [1], [2], [1, 2] ]
        /// </summary>
        public IEnumerator<IEnumerator<T>> Subset => subsetting();

        /// <summary> генерация всех возможных перестановок 
        /// 
        /// Входное перечисление: [1, 2, 3]
        /// 
        /// Выходное перечисление: [ [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1,2], [3, 2, 1] ]
        /// </summary>
        public IEnumerator<IEnumerator<T>> Permutations => permutationing();


        private IEnumerator<IEnumerator<T>> combination(int k)
            //wtf with casts
        {
            var ret = Enumerable.Empty<IEnumerator<T>>();

            for(int i=0; i< _elems.Count()-1 ; i++)
            {
                T it = _elems[i];

                T[] comb = new T[k];

                int iter = 0;

                for (int j=i; j< _elems.Count();j++)
                {
                    
                    if (iter+1==k-1) {
                        iter = 0;
                        comb[k - 1] = it; 
                        Array.Sort(comb);
                        ret = ret.Union( (IEnumerable<IEnumerator<T>>) comb.GetEnumerator());
                    }
                    comb[iter++] = _elems[j];

                }

            }

            return ret.GetEnumerator();
        }

        private IEnumerator<IEnumerator<T>> subsetting()
        {
            var ret = Enumerable.Empty<IEnumerator<T>>();

            int k = _elems.Count();

            for(int dim=2;dim<=k; dim++)
            for (int i = 0; i < _elems.Count() - 1; i++)
            {
                T it = _elems[i];

                T[] comb = new T[dim];

                int iter = 0;
                ret.Union((IEnumerable<IEnumerator<T>>)((new T[] {it}).GetEnumerator()));

                for (int j = i; j < _elems.Count(); j++)
                {
                    if (iter + 1 == dim - 1)
                    {
                        iter = 0;
                        comb[dim - 1] = it;
                        Array.Sort(comb);
                        ret = ret.Union((IEnumerable<IEnumerator<T>>)comb.GetEnumerator());
                    }
                    comb[iter++] = _elems[j];

                }

            }

            return ret.GetEnumerator();
        }

        private IEnumerator<IEnumerator<T>> permutationing()
        {
            var ret = Enumerable.Empty<IEnumerator<T>>();

            var under = Enumerable.Empty<T>();

            return ret.GetEnumerator();
        }


        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
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
