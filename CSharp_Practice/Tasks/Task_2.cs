using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

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
    public sealed class Logger :IDisposable
    {
        private  ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();

        private  StreamWriter _fs;
        public  enum Severity
        {
            race, debug,
            information,
            warning, error, critical
        }

        public Logger(string filepath) =>
        
            _fs = new StreamWriter(filepath, true);
        

        public void Set_file(string filepath)
        {
            Dispose();
            _fs = new StreamWriter(filepath, true);
            return;
        }

        public int Log(string data, Severity severity)
        {
            _cacheLock.EnterWriteLock();
            //DNC Double not 
            try { 
                _fs.Write($"{Environment.NewLine}[{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}] " +
                    $"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] [{severity}]: {data}");
            
                return 0;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                _cacheLock.ExitWriteLock();
            }
            
        }
        ~Logger()=>
            _fs.Dispose();
        

        public void Dispose()//ToDO ;_cacheLock clear
        {
            _fs.Flush();
            _fs.Close();
            _fs.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    class Pragma
    {
        delegate void Message();
        Message _mes;
        private struct syslog
        {
            public string data { get; set; }
            public Logger.Severity severity { get; set; }
        }

         syslog _syslog;
        static Random _random = new Random();
        static Array _values = Enum.GetValues(typeof(Logger.Severity));

        public Logger Logger { get; }
        public Pragma(string path)
        => Logger = new Logger(path);
        

        private void DeployMorning()
        {
            _syslog.data = "Morning Deploy";
            _syslog.severity = (Logger.Severity)_values.GetValue(_random.Next(_values.Length));
        }
        private void DeployEvening()
        {
            _syslog.data= "Evening Deploy" ;
            _syslog.severity = (Logger.Severity) _values.GetValue(_random.Next(_values.Length));
        }

        public int DoSmth()
        {
            try {
                if (Logger==null) throw new DirectoryNotFoundException( "Logger or filepath can be equal null pls relaunch Pragma");

            
            if (DateTime.Now.Hour < 12)
                _mes = DeployMorning;
            
            else
                _mes = DeployEvening;
            
            _mes();
            Logger.Log(_syslog.data,_syslog.severity);
            return 0;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        =>  Logger.Dispose();
    }
}
