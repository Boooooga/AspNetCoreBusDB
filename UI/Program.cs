using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using LogLevel = NLog.LogLevel;

namespace UI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception exception)
			{
				logger.Error(exception, "App run was unsuccessful");
				throw;
			}
			finally
			{
				LogManager.Shutdown();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				}).UseNLog();
	}
}
