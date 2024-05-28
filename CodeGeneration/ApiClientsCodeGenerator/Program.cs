using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Api.Enums;
using SpaceApp.Dev.ApiToMobile;
using SpaceApp.Dev.ApiToMobile.Converters.Android;
using SpaceApp.Dev.ApiToMobile.Settings;

namespace CodeGeneration.ApiClientsCodeGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			var outputDirectory = "output";
			if (Directory.Exists(outputDirectory))
			{
				try
				{
					Directory.Delete(outputDirectory, true);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Невозможно очистить директорию {outputDirectory}: {ex}");
				}
			}
			using var loggerFactory =
				LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));
			new CodeGenerator().Generate(new GeneratorSettings
			{
				Assembly = typeof(OperationStatus).Assembly,
				OutputDirectory = outputDirectory,
				AndroidPackage = new AndroidPackage("ru.spaceapp.test.android.sal"),
				FlutterPackageName = "sal",
				ApiClientMethodTypes = ApiClientMethodType.Asynchronous,
				PropertiesNamingPolicy = ApiNamingPolicy.Identity,
				EnumSerializationStrategy = EnumSerializationStrategy.Integer,
				DateSerializationStrategy = DateSerializationStrategy.Iso8601,
				DecimalSerializationStrategy = DecimalSerializationStrategy.String,
				PathProvider = new DefaultGeneratedPathProvider(),
				RouteTransformer = new CustomRouteTransformer(),
				Logger = loggerFactory.CreateLogger<Program>()
			});
		}
	}
}
