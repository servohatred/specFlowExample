using NLog;
using NLog.Targets;
using OpenQA.Selenium;
using System;
using System.IO;
using TestBDD.Utilities.Browser;
using LogLevel = NLog.LogLevel;

namespace TestBDD.Evidences
{
    public static class Evidence
    {
        private static Logger testLogger;
        private static Logger businessLogger;
        private static Logger replicaLogger;
        public static string evidencePath { get; set; }
        public static string imagesPath { get; set; }
        public static string logsPath { get; set; }
        public static int imageCount = 1;
        public static Screenshot file { get; set; }
        public static string lastImagePath { get; set; }

        public static void init(string path, string projectName)
        {
            string timeTag = DateTime.Now.ToString("-yyyy-MM-dd'T'HH-mm");
            evidencePath = path + "/" + projectName + timeTag;
            imagesPath = evidencePath + "/images";
            logsPath = evidencePath + "/logs"; 
            InitLogs(logsPath);
        }

        public static void init(string path, string projectName,string feature,string scenario)
        {
            string timeTag = DateTime.Now.ToString("-yyyy-MM-dd'T'HH-mm");
            evidencePath = path + "/" + projectName + timeTag;
            imagesPath = evidencePath + "/" + feature.Replace(" ", "_") + "/" + scenario.Replace(" ","_") + "/images";
            logsPath = evidencePath + "/" + feature.Replace(" ", "_") + "/" + scenario.Replace(" ", "_") + "/logs";
            if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
            if (!Directory.Exists(logsPath)) Directory.CreateDirectory(logsPath);
            InitLogs(logsPath);
        }
    
        public static void InitLogs(string path)
        {
            var config = new NLog.Config.LoggingConfiguration();
            FileTarget testLogFile = new FileTarget("TestLog") { FileName = path + "/TestLog.txt" };
            FileTarget businessLogFile = new FileTarget("BusinessLog") { FileName = path + "/BusinessLog.txt" };
            FileTarget replicaLogFile = new FileTarget("ReplicaLog") { FileName = path + "/ReplicaLog.txt" };
            ConsoleTarget logconsole = new ConsoleTarget("logconsole");
            testLogFile.Layout = "[${longdate} - {TestLog} - ${uppercase:${level}}]: ${message} ${exception}";
            businessLogFile.Layout = "[${longdate} - {BusinessLog} - ${uppercase:${level}}]: ${message} ${exception}";
            replicaLogFile.Layout = "[${longdate} - {ReplicaLog} - ${uppercase:${level}}]: ${message} ${exception}";
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, testLogFile, "TestLog");
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, businessLogFile, "BusinessLog");
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, replicaLogFile, "ReplicaLog");
            LogManager.Configuration = config;
            testLogger = LogManager.GetLogger("TestLog");
            businessLogger = LogManager.GetLogger("BusinessLog");
            replicaLogger = LogManager.GetLogger("EvidenceLog");
        }

        public static class TestLog
        {
            public static void Info(string message)
            {
                testLogger.Info(message);

            }
            public static void Error(string message,Exception exception)
            {
                testLogger.Error(exception,message);

            }

            public static void Error(string message)
            {
                testLogger.Error(message);

            }
        }

        public static class BusinessLog
        {
            public static void Info(string message)
            {
                businessLogger.Info(message);

            }
            public static void Error(string message, Exception exception)
            {
                businessLogger.Error(exception, message);

            }

            public static void Error(string message)
            {
                businessLogger.Error(message);

            }
        }

        public static class ReplicaLog
        {
            public static void Info(string message)
            {
                replicaLogger.Info(message);

            }
            public static void Error(string message, Exception exception)
            {
                replicaLogger.Error(exception, message);

            }

            public static void Error(string message)
            {
                replicaLogger.Error(message);

            }
        }
        public static Screenshot GetScreenshot(IWebDriver driver,ExWebElement webElement)
        {
            ExWebElement.WrappedDriver = driver;
            string BORDER_SCRIPT = @"arguments[0].style = ""border-width: 4px; border-style: solid; border-color: red"";";
            string NO_BORDER_SCRIPT = @"arguments[0].style.cssText = ""border-width: none; border-style: none; border-color: none"";";
            string CENTER_SCRIPT = @"arguments[0].scrollIntoView(true);";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(CENTER_SCRIPT, new object[] { webElement.webElement });
            js.ExecuteScript(BORDER_SCRIPT, new object[] { webElement.webElement });
            file = ((ITakesScreenshot)driver).GetScreenshot();
            lastImagePath = imagesPath + "/image" + imageCount + ".png";
            file.SaveAsFile(lastImagePath, ScreenshotImageFormat.Png);
            js.ExecuteScript(NO_BORDER_SCRIPT, new object[] { webElement.webElement });
            imageCount++;
            return file;
        }
        public static Screenshot GetScreenshot(IWebDriver driver)
        {
            file = ((ITakesScreenshot)driver).GetScreenshot();
            lastImagePath = imagesPath + "/image" + imageCount + ".png";
            file.SaveAsFile(lastImagePath, ScreenshotImageFormat.Png);
            imageCount++;
            return file;
        }
    }
}
