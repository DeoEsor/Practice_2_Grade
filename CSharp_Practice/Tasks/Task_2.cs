using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks
{
    /*Реализовать класс логгера. Конструктор логгера принимает на вход путь
к файлу, в который будут записываться логи с различными уровнями
жёсткости (severity; от наименее к наиболее жёсткому: trace, debug,
information, warning, error, critical; severity описан в виде enum). Метод Log
позволяет залоггировать в файл переданную строку data с переданным
severity в следующем формате:
[<Date> <Time>] [<severity>]: <data>
В классе реализовать необходимые интерфейсы.
    */
    class Logger
    {
        string _filepath;

        private static Logger instance;

        enum Severity
        {
            race, debug,
            information,
            warning, error, critical
        }

        private Logger(string path)
        {
            _filepath = path;
        }

        public static Logger getInstance(string name)
        {
            if (instance == null)
                instance = new Logger(name);
            return instance;
        }

        public int Log(string data)
        {

            return 0;
        }

    }

    class Computer
    {
        public Logger Logger { get; set; }
        public void Launch(string path)
        {
            Logger = Logger.getInstance(path);
        }
    }
}
