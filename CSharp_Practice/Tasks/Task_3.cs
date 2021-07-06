using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
namespace Tasks
{
    /*На языке C# реализовать класс, представляющий собой универсальный
(шаблонный) кэш приложения. Конструктор кэша принимает два
параметра: время жизни записей (типа TimeSpan) и максимальный размер
(типа int). Кэш имеет два публичных метода Get и Save.

Записи из кэша (вместе с ключом) удаляются, когда заканчивается их
время жизни. Также, если при добавлении записи кэш имеет
максимальный размер, то добавляемая запись замещает самую старую
запись в кэше. Продемонстрировать работу класса.
*/
class Cash<T>
{
        static Dictionary<string,T> _cash = new System.Collections.Generic.Dictionary<string,T>();

        static SortedDictionary<int, string> _timetable = new SortedDictionary<int, string>();

        int _size;

        static TimeSpan _time;

        private static System.Timers.Timer aTimer;

        public Cash(TimeSpan time, int size)
        {
            SetTimer( time);
            _time = time;
            _size = size;
        }

        private static void SetTimer(TimeSpan time)
        {
            aTimer = new System.Timers.Timer(time.Ticks/5);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (_cash.Count == 0) return;
            foreach(var date in _timetable)
            {
                int interval=date.Key - (int)_time.Ticks / 5;
                if (interval <= 0) {
                    _cash.Remove(date.Value);
                    _timetable.Remove(date.Key);
                }
                else//TODO: ...oh... create new dictionary with calculations, after it _cash= {new dcitionary} 
                // UPD:: mb random time of calling Save can save situation
                {
                    _timetable[interval] = date.Value;
                    _timetable.Remove(date.Key);
                }
            }
            return;
        }

        /// <summary>
        /// - вызов метода Get возвращает данные из кэша по заданному ключу
        /// (типа string); при отсутствии в кэше переданного ключа, должно
        /// быть сгенерировано исключение типа KeyNotFoundException.
        /// </summary>
        /// <param name="key">Заданный ключ (типа string)</param>
        /// <returns> 0 if process for sucessful eles -1 or Error Code  with exception</returns>
        public T Get(string key)
        {
            try
            {
                if (!_cash.ContainsKey(key)) throw new KeyNotFoundException("Value associated was null");

                else return _cash[key];
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return (T) new Object();
            }
        }
        /// <summary>
        /// при вызове метода Save переданные данные сохраняются в памяти
        /// кэша по заданному ключу(типа string); при наличии в кэше
        /// переданного ключа, должно быть сгенерировано исключение типа ArgumentException.
        /// </summary>
        /// <param name="data"> Данные типа хранимые в кэше </param>
        /// <param name="key"> Ключ типа string, с которым должно быть ассоциированы данные</param>
        /// <returns></returns>
        public int Save(T data,string key)
        {
            try {
                if (_cash.ContainsKey(key)) throw new ArgumentException("Key was restored for another day");
                if (_cash.Count == _size) {
                    int lowwest = (int)_time.Ticks + 1;
                    foreach (int it in _timetable.Keys)
                        if(it<lowwest) lowwest = it;

                    _cash.Remove(_timetable[lowwest]);
                    _timetable.Remove(lowwest);


                    _cash[key] = data;
                    _timetable[(int)_time.Ticks] = key;
                }
                else
                {
                    _cash[key] = data;
                    _timetable[(int)_time.Ticks]= key;
                }
                return 0;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return -1;
            }
        }
    }
}
