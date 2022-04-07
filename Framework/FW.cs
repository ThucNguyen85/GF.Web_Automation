using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gf.Web.Automation.Framework.Logging;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Gf.Web.Automation.Framework
{
    public class FW
    {
        public static string WORKSPACE_DIRECTORY = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

        public static Logger Log => _logger ?? throw new NullReferenceException("_logger is null, call SetLogger() first.");
        public static FwConfig fwConfig => _fwConfig ?? throw new NullReferenceException("_fwConfigu is null, call SetConfig() first.");
        public static DirectoryInfo CurrentTestDirectory;
        private static Logger _logger;
        private static FwConfig _fwConfig;
        public static DirectoryInfo CreateTestResultDirectory()
        {
            var directory = WORKSPACE_DIRECTORY + "TestResults";
            if (!Directory.Exists(directory))
            {
                return Directory.CreateDirectory(directory);
            }
            return null;
            /*if (Directory.Exists(directory))
            {
                try
                {
                    Directory.Delete(directory, recursive: true);
                }
                catch (System.IO.IOException)
                {
                    Console.WriteLine("The directory is not empty.");
                }
                
            }
            return Directory.CreateDirectory(directory);*/
        }

        public static void SetLogger()
        {
            lock (_setLoggerLock)
            {
                var testResultsDir = WORKSPACE_DIRECTORY + "TestResults";
                var testName = TestContext.CurrentContext.Test.Name;            
                /*var fullPath = $"{testResultsDir}/{testName}{DateTime.Now.Date.ToString().RemoveSpecialCharacters()}";

                if (Directory.Exists(fullPath))
                {
                    CurrentTestDirectory = Directory.CreateDirectory(fullPath + TestContext.CurrentContext.Test.ID);
                }
                else
                {
                    CurrentTestDirectory = Directory.CreateDirectory(fullPath);
                }*/
                _logger = new Logger(testName, testResultsDir + $"/{testName}{DateTime.Now.ToLocalTime().ToString().RemoveSpecialCharacters()}log.txt");
            }
        }

        private static object _setLoggerLock = new object();

        public static void SetConfig()
        {
            if(_fwConfig == null)
            {
                var jsonStr = File.ReadAllText(WORKSPACE_DIRECTORY + "/framework-config.json");
                _fwConfig = JsonConvert.DeserializeObject<FwConfig>(jsonStr);
            }
        }
    }
    public static class MethodExtensionHelper
    {
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
