using System.Diagnostics.Tracing;

namespace Logging
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Log.MyFirstEvent("Hi", 1);
        }
    }

    [EventSource(Name = "Logging-Demo")]
    class Logger : EventSource
    {
        public void MyFirstEvent(string name, int id)
        {
            WriteEvent(1, name, id);
        }
        public void MySecondEvent(int id)
        {
            WriteEvent(2, id);
        }
        public static Logger Log = new Logger();
    }
}
