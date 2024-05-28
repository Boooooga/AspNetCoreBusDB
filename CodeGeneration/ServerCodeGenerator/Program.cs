using System;
using CodeGeneration.ServerCodeGenerator.Enums;
using CodeGeneration.ServerCodeGenerator.MergeUtilities;

namespace CodeGeneration.ServerCodeGenerator
{
	internal class Program
	{
		internal static void Main(string[] args)
		{
			var generator = new CodeGenerator("config.xml", Console.Out, new KDiff3MergeUtility());
			if (generator.ExistingFilesProcessMode == ExistingFilesProcessMode.Overwrite)
			{
				Console.WriteLine("ВНИМАНИЕ! Все существующие файлы будут перезаписаны. Нажмите ENTER для продолжения.");
				Console.ReadLine();
			}
			generator.Generate();
			Console.WriteLine("Завершено");
			Console.ReadLine();
		}
	}
}

