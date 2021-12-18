using NLog;

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
			var config = new NLog.Config.LoggingConfiguration();

			// Targets where to log to: File and Console
			var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "log.txt" };
			var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

			// Rules for mapping loggers to targets            
			config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
			config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

			// Apply config           
			LogManager.Configuration = config;
		}
	}
}