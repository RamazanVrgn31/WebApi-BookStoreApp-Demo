﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Services.Contrats;

namespace Services.Concrete
{
    public class LoggerManager : ILoggerService
    {
        private static ILogger logger= LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string message) => logger.Error(message);
        public void LogIinfo(string message)  => logger.Info(message);

        public void LogWarning(string message) => logger.Warn(message);
    }
}
