using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gf.Web.Automation.Framework.Logging
{
    public class Logger
    {
        private readonly string _filePath;

        public Logger(string testname, string filepath)
        {
            _filePath = filepath;

            using var log = File.AppendText(_filePath);
            log.WriteLine($"Staring TimeStamp: {DateTime.Now.ToLocalTime()}");
            log.WriteLine($"Test: {testname}");
        }

        public void Info(string message)
        {
            WriteLine($"[Info]: {message}");
        }

        public void Step(string message)
        {
            WriteLine($"    [Step]: {message}");
        }

        public void Error(string message)
        {
            WriteLine($"[ERROR]: {message}");
        }

        public void Warning(string message)
        {
            WriteLine($"[WARNING]: {message}");
        }

        public void Fatal(string message)
        {
            WriteLine($"[FATAL]: {message}");
        }


        public void WriteLine(string text)
        {
            using var log = File.AppendText(_filePath);
            log.WriteLine(text);
        }

        public void Write(string text)
        {
            using var log = File.AppendText(_filePath);
            log.Write(text);
        }
    }
}
