using System;
using System.Reflection;
using log4net;

namespace Logger
{
	public enum LogType
	{
		Debug,
		Info,
		Warn,
		Error,
		Fatal
	}

	public static class LogHandler
	{
		public static void Log(string nameOfLogger, LogType type, object message)
		{
			var log = LogManager.GetLogger(nameOfLogger);
			switch (type)
			{
				case LogType.Debug:
					log.Debug(message);
					break;
				case LogType.Info:
					log.Info(message);
					break;
				case LogType.Warn:
					log.Warn(message);
					break;
				case LogType.Error:
					log.Error(message);
					break;
				case LogType.Fatal:
					log.Fatal(message);
					break;
			}
		}

		public static void Configure()
		{
			log4net.Config.XmlConfigurator.Configure();
		}
	}
}