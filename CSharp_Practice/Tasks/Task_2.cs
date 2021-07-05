using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tasks
{
    /*Реализовать класс логгера. Конструктор логгера принимает на вход путь
        к файлу, в который будут записываться логи с различными уровнями
        жёсткости (severity; от наименее к наиболее жёсткому: trace, debug,
        information, warning, error, critical; severity описан в виде enum). Метод Log
        позволяет залоггировать в файл переданную строку data с переданным
        severity в следующем формате:
        [<Date> <Time>] [<severity>]: <data>
        В классе реализовать необходимые интерфейсы.*/
    internal class Logger
    {
        static string _filepath;

        private static Logger instance;

        public enum Severity
        {
            race, debug,
            information,
            warning, error, critical
        }

        protected Logger()
        {
        }

        private Logger(string filepath)
        {
            _filepath = filepath;
        }

        public static Logger getInstance(string name)
        {
            if (instance == null)
                instance = new Logger(name);
            return instance;
        }

        public static int Log(string data, Severity severity)
        {
            using (FileStream fs = new FileStream(_filepath, FileMode.Append, FileAccess.Write))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes($"{Environment.NewLine}[{DateTime.Now}] [{severity}]: {data}");
                fs.Write(array, 0, array.Length);
                fs.Flush();
                fs.Close();
            }
            
            return 0;
        }
        
    }

    class Pragma : Logger
    {
        delegate void Message();
        private struct syslog
        {
            public string data { get; set; }
            public Severity severity { get; set; }
        }

        static syslog _syslog;
        static Random random = new Random();
        static Array values = Enum.GetValues(typeof(Severity));

        public Logger Logger { get; set; }
        public void Launch(string path)
        {
            Logger = Logger.getInstance(path);
        }

        private void  DeployMorning()
        {
            _syslog.data = "Morning Deploy";
            _syslog.severity = (Severity)values.GetValue(random.Next(values.Length));
        }
        private static void DeployEvening()
        {
            _syslog.data= "Evening Deploy" ;
            _syslog.severity = (Severity) values.GetValue(random.Next(values.Length));
        }

        public int DoSmth()
        {
            Message mes;
            if (DateTime.Now.Hour < 12)
                mes = DeployMorning;
            
            else
                mes = DeployEvening;
            
            mes();
            Logger.Log(_syslog.data,_syslog.severity);
            return 0;
        }
    }
}
