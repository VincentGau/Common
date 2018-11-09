using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Logger
    {
        private static ILog logger;

        static Logger()
        {
            Init();
        }

        private static void Init()
        {
            string useDefaultLog = ConfigurationManager.AppSettings["useDefaultLog"];

            if (String.IsNullOrEmpty(useDefaultLog) || useDefaultLog.Equals("true"))
            {
                //setDefaultLogger();
                SetDefaultLogger();
            }

            else
            {
                string logConfigFile = ConfigurationManager.AppSettings["LogConfigFile"];
                string loggerName = ConfigurationManager.AppSettings["LoggerName"];

                if (string.IsNullOrEmpty(logConfigFile))
                {
                    throw new ArgumentException("No log config file was assigned.");
                }

                if (string.IsNullOrEmpty(loggerName))
                {
                    throw new ArgumentException("Logger name not assigned.");
                }

                string filePath = string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, logConfigFile);

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("No log config file was found.");
                }

                XmlConfigurator.ConfigureAndWatch(new FileInfo(filePath));

                logger = LogManager.GetLogger(loggerName);
            }
            
        }

        /// <summary>
        /// 如果不想自定义log4net 配置文件，该方法提供一个默认的logger,该logger直接写文件；
        /// </summary>
        private static void setDefaultLogger()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Shutdown();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%d [%t] %-5p %c [%x] - %m%n";
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender
            {
                AppendToFile = true,
                File = @"Log\Log_",
                Layout = patternLayout,
                MaxSizeRollBackups = 100,
                MaximumFileSize = "1GB",
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyy-MM-dd'.log'",
                StaticLogFileName = false
            };
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

        /// <summary>
        /// 如果不想自定义log4net 配置文件，该方法提供一个默认的logger,该logger直接写数据库；
        /// </summary>
        private static void SetDefaultLogger()
        {
            AdoNetAppender appender = new AdoNetAppender();
            appender.BufferSize = 0;
            appender.ConnectionType = @"System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
            appender.ConnectionString = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=log;integrated security=true;persist security info=True;";
            appender.CommandText = @"INSERT INTO Log ([Date],[Thread],[Level],[Logger],[Message],[Exception],  [ComputerName]) VALUES (@log_date, @thread, @log_level, @logger, @message, @exception, @ComputerName)";

            appender.AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = "@log_date",
                DbType = System.Data.DbType.DateTime,
                Size = 100,
                //Layout = new RawLayoutConverter().ConvertFrom(new PatternLayout("%date{yyyy'-'MM'-'dd HH':'mm':'ss'.'fff}")) as IRawLayout,
                Layout = new RawTimeStampLayout(),
            });

            appender.AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = "@thread",
                DbType = System.Data.DbType.String,
                Size = 255,
                //Layout = new RawLayoutConverter().ConvertFrom(new PatternLayout("%thread")) as IRawLayout,
                Layout = new Layout2RawLayoutAdapter(new PatternLayout("%thread")),
        });

            appender.AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = "@log_level",
                DbType = System.Data.DbType.String,
                Size = 50,
                Layout = new RawLayoutConverter().ConvertFrom(new PatternLayout("%level")) as IRawLayout
            });

            appender.AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = "@logger",
                DbType = System.Data.DbType.String,
                Size = 255,
                Layout = new RawLayoutConverter().ConvertFrom(new PatternLayout("%logger")) as IRawLayout
            });

            appender.AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = "@message",
                DbType = System.Data.DbType.String,
                Size = 4000,
                Layout = new RawLayoutConverter().ConvertFrom(new PatternLayout("%message")) as IRawLayout
            });

            appender.AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = "@exception",
                DbType = System.Data.DbType.String,
                Size = 2000,
                Layout = new RawLayoutConverter().ConvertFrom(new ExceptionLayout()) as IRawLayout
            });

            appender.AddParameter(new AdoNetAppenderParameter()
            {
                ParameterName = "@ComputerName",
                DbType = System.Data.DbType.String,
                Size = 255,
                Layout = new RawLayoutConverter().ConvertFrom(new PatternLayout("%property{log4net:HostName}")) as IRawLayout,
            });

            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);
            logger = LogManager.GetLogger(ConfigHelper.GetString("loggerName") ?? "DefaultLogger");
        }

        #region public methods

        public static void Info(string msg)
        {
            logger.Info(msg);
        }

        public static void Info(string msg, Exception e)
        {
            logger.Info(msg, e);
        }

        public static void Warn(string msg)
        {
            logger.Warn(msg);
        }

        public static void Warn(string msg, Exception e)
        {
            logger.Warn(msg, e);
        }

        public static void Error(string msg)
        {
            logger.Error(msg);
        }

        public static void Error(string msg, Exception e)
        {
            logger.Error(msg, e);
        }

        public static void Debug(string msg)
        {
            logger.Debug(msg);
        }

        public static void Debug(string msg, Exception e)
        {
            logger.Debug(msg, e);
        }

        public static void Fatal(string msg)
        {
            logger.Fatal(msg);
        }

        public static void Fatal(string msg, Exception e)
        {
            logger.Fatal(msg, e);
        }

        #endregion
    }
}
