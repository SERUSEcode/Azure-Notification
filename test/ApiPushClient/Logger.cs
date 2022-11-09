using System;
using System.IO;

namespace ApiPushClient
{
    public class Logger
    {
        private TextWriter tw;
        public Logger(TextWriter output)
        {
            tw = output;
        }
        internal void Log(string msg)
        {
            ResetCursor();
            tw.WriteLine(msg);
        }
        internal void Write(string msg)
        {
            ResetCursor();
            tw.Write(msg);
        }
        private void ResetCursor()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
