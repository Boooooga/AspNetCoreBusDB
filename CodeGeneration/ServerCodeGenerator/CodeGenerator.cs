using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using CodeGeneration.ServerCodeGenerator.Enums;
using CodeGeneration.ServerCodeGenerator.MergeUtilities;
using CodeGeneration.ServerCodeGenerator.Templates;
using Dal.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CodeGeneration.ServerCodeGenerator
{
	internal class CodeGenerator
	{
		internal TextWriter MessagesWriter { get; }
		internal MergeUtility MergeUtility { get; set; }
		internal string SolutionFolderPath { get; }
		internal ExistingFilesProcessMode ExistingFilesProcessMode { get; }
		internal IList<EntityDescription> Entities { get; }
		internal int MaxLineWidth { get; set; }

		internal CodeGenerator(string configPath, TextWriter messagesWriter = null, 
			MergeUtility mergeUtility = null)
		{
			MessagesWriter = messagesWriter;
			MergeUtility = mergeUtility;
			var config = new XmlDocument();
			config.Load(configPath);
			var rootNode = config.DocumentElement;
			Entities = new List<EntityDescription>();
			foreach (XmlNode node in rootNode.ChildNodes)
			{
				switch (node.Name)
				{
					case "generationSettings":
						SolutionFolderPath = node.Attributes["solutionFolderPath"]?.Value;
						ExistingFilesProcessMode = Enum.TryParse(node.Attributes["existingFilesProcessMode"]?.Value, 
							out ExistingFilesProcessMode processMode) ? processMode : ExistingFilesProcessMode.Skip;
						MaxLineWidth = int.TryParse(node.Attributes["maxLineWidth"]?.Value, out int maxLineWidth) ? maxLineWidth : 120;
						MaxLineWidth = Math.Max(1, MaxLineWidth);
						break;
					case "entities":
						foreach (XmlNode entityNode in node.SelectNodes("entity"))
						{
							var entity = ReadEntity(entityNode);
							if (entity != null)
							{
								Entities.Add(entity);
							}
						}
						break;
				}
			}
		}

		internal void Generate()
		{
			var project = new MicrosoftBuildProject(Path.Combine(SolutionFolderPath, "Entities\\Entities.csproj"));
			GenerateEntities(project);
			project.Save();

			project = new MicrosoftBuildProject(Path.Combine(SolutionFolderPath, "Common\\Common.csproj"));
			GenerateSearchClasses(project);
			project.Save();

			project = new MicrosoftBuildProject(Path.Combine(SolutionFolderPath, "Dal\\Dal.csproj"));
			GenerateDataAccessClasses(project);
			project.Save();

			project = new MicrosoftBuildProject(Path.Combine(SolutionFolderPath, "BL\\BL.csproj"));
			GenerateBusinessLogicClasses(project);
			project.Save();

			project = new MicrosoftBuildProject(Path.Combine(SolutionFolderPath, "UI\\UI.csproj"));
			GenerateModels(project);
			GenerateControllers(project);
			project.Save();
			GenerateViews(project);
		}

		private void GenerateEntities(MicrosoftBuildProject project)
		{
			foreach (var description in Entities)
			{
				if ((description.Files & GeneratedFiles.Entity) == GeneratedFiles.None)
					continue;
				var fileName = description.Name + ".cs";
				var item = new MicrosoftBuildProject.Item(fileName, "Compile");
				CreateFileInProject(project, item, 
					new EntityTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
					new EntityTemplate(description, MaxLineWidth).TransformText(), 
					$"Генерация сущности {fileName}...");
			}
		}

		private void GenerateSearchClasses(MicrosoftBuildProject project)
		{
			foreach (var description in Entities)
			{
				if ((description.Files & GeneratedFiles.SearchParams) == GeneratedFiles.None)
					continue;
				var fileName = description.PluralName + "SearchParams" + ".cs";
				var item = new MicrosoftBuildProject.Item($"Search\\{fileName}", "Compile");
				CreateFileInProject(project, item, 
					new SearchParamsTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
					new SearchParamsTemplate(description, MaxLineWidth).TransformText(), 
					$"Генерация поискового класса {fileName}...");
			}
		}

		private void GenerateDataAccessClasses(MicrosoftBuildProject project)
		{
			foreach (var description in Entities)
			{
				if ((description.Files & GeneratedFiles.Dal) == GeneratedFiles.None)
					continue;
				var fileName = description.PluralName + "Dal.cs";
				var item = new MicrosoftBuildProject.Item(fileName, "Compile");
				CreateFileInProject(project, item, 
					new DalTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
					new DalTemplate(description, MaxLineWidth).TransformText(), 
					$"Генерация класса доступа к данным {fileName}...");
			}
		}

		private void GenerateBusinessLogicClasses(MicrosoftBuildProject project)
		{
			foreach (var description in Entities)
			{
				if ((description.Files & GeneratedFiles.BL) == GeneratedFiles.None)
					continue;
				var fileName = description.PluralName + "BL.cs";
				var item = new MicrosoftBuildProject.Item(fileName, "Compile");
				CreateFileInProject(project, item, 
					new BLTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
					new BLTemplate(description, MaxLineWidth).TransformText(), 
					$"Генерация класса бизнес-логики {fileName}...");
			}
		}

		private void GenerateModels(MicrosoftBuildProject project)
		{
			foreach (var description in Entities)
			{
				if ((description.Files & GeneratedFiles.Model) == GeneratedFiles.None)
					continue;
				var fileName = description.Name + "Model.cs";
				var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Models\\{fileName}", "Compile");
				CreateFileInProject(project, item, 
					new ModelTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
					new ModelTemplate(description, MaxLineWidth).TransformText(), 
					$"Генерация модели {fileName}...");
			}
		}

		private void GenerateControllers(MicrosoftBuildProject project)
		{
			foreach (var description in Entities)
			{
				if ((description.Files & GeneratedFiles.Controller) == GeneratedFiles.None)
					continue;
				var fileName = description.PluralName + "Controller.cs";
				var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Controllers\\{fileName}", "Compile");
				CreateFileInProject(project, item, 
					new ControllerTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
					new ControllerTemplate(description, MaxLineWidth).TransformText(), 
					$"Генерация контроллера {fileName}...");
			}
		}

		private void GenerateViews(MicrosoftBuildProject project)
		{
			foreach (var description in Entities)
			{
				if ((description.Files & GeneratedFiles.ViewIndex) == GeneratedFiles.ViewIndex)
				{
					var fileName = "Index.cshtml";
					var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Views\\{description.PluralName}\\{fileName}", "Content");
					CreateFileInProject(project, item, 
						new ViewIndexTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
						new ViewIndexTemplate(description, MaxLineWidth).TransformText(), 
						$"Генерация представления {description.PluralName}\\{fileName}...");
				}
				if ((description.Files & GeneratedFiles.ViewUpdate) == GeneratedFiles.ViewUpdate)
				{
					var fileName = "Update.cshtml";
					var item = new MicrosoftBuildProject.Item($"Areas\\Admin\\Views\\{description.PluralName}\\{fileName}", "Content");
					CreateFileInProject(project, item, 
						new ViewUpdateTemplate(description.ExcludeNewProperties(), MaxLineWidth).TransformText(),
						new ViewUpdateTemplate(description, MaxLineWidth).TransformText(),
						$"Генерация представления {description.PluralName}\\{fileName}...");
				}
			}
		}

		private void CreateFileInProject(MicrosoftBuildProject project, MicrosoftBuildProject.Item item, string oldFileContent, string currentFileContent, string infoMessage)
		{
			MessagesWriter?.WriteLine(infoMessage);
			var projectDirectory = new FileInfo(project.Path).Directory.FullName;
			var filePath = Path.Combine(projectDirectory, item.Path);
			new FileInfo(filePath).Directory.Create();
			project.AddItem(item);
			var fileContent = currentFileContent;
			if (!File.Exists(filePath) || ExistingFilesProcessMode != ExistingFilesProcessMode.Skip)
			{
				if (File.Exists(filePath) && ExistingFilesProcessMode == ExistingFilesProcessMode.Merge && MergeUtility != null)
				{
					var generatedFilePath = filePath + ".generated.tmp";
					File.WriteAllText(generatedFilePath, currentFileContent, Encoding.UTF8);
					var oldFilePath = filePath + ".old.tmp";
					File.WriteAllText(oldFilePath, oldFileContent, Encoding.UTF8);
					var mergedFilePath = filePath + ".merged.tmp";
					File.Delete(mergedFilePath);
					MergeUtility.PerformMerge(oldFilePath, generatedFilePath, filePath, mergedFilePath);
					if (File.Exists(mergedFilePath))
					{
						var sourceContent = File.ReadAllText(filePath, Encoding.UTF8);
						var mergedContent = File.ReadAllText(mergedFilePath, Encoding.UTF8);
						MessagesWriter?.WriteLine(sourceContent != mergedContent
							? "Выполняем объединение изменений."
							: "Файл не изменен.");
						fileContent = mergedContent;
						File.Delete(mergedFilePath);
					}
					else
					{
						MessagesWriter?.WriteLine("Объединение отменено, используем новый файл.");
					}
					File.Delete(generatedFilePath);
					File.Delete(oldFilePath);
				}
				File.WriteAllText(filePath, fileContent, Encoding.UTF8);
			}
			else
			{
				MessagesWriter?.WriteLine("Файл существует, пропускаем.");
			}
		}

		private EntityDescription ReadEntity(XmlNode node)
		{
			var entityName = node.Attributes?["name"].Value;
			if (string.IsNullOrEmpty(entityName))
			{
				MessagesWriter?.WriteLine("Не указано имя сущности");
				return null;
			}
			using var dbContext = new DefaultDbContext();
			var efType = dbContext.Model.FindEntityType("Dal.DbModels." + entityName);
			if (efType == null)
			{
				MessagesWriter?.WriteLine($"Не удалось загрузить метаданные о типе {entityName} из модели EF");
				return null;
			}
			var entity = new EntityDescription
			{
				Name = entityName,
				Properties = new List<PropertyDescription>(),
				PluralName = efType.GetTableName(),
				Files = GeneratedFiles.All
			};
			var filesNodes = node.SelectSingleNode("files")?.SelectNodes("file");
			if (filesNodes != null)
			{
				var excludedFiles = GeneratedFiles.None;
				foreach (XmlNode fileNode in filesNodes)
				{
					if (!Enum.TryParse(fileNode?.Attributes?["type"].Value, out GeneratedFiles fileType))
						continue;
					if (bool.TryParse(fileNode.InnerText, out bool nodeValue))
					{
						if (!nodeValue)
							excludedFiles |= fileType;
					}
				}
				entity.Files ^= excludedFiles;
			}
			var propertiesNodes = node.SelectSingleNode("properties")?.SelectNodes("property")?.Cast<XmlNode>()
				.ToDictionary(item => item.Attributes["dbName"].Value, item => item);
			foreach (var property in efType.ClrType.GetProperties())
			{
				if (!property.GetGetMethod().IsVirtual && property.GetGetMethod().IsPublic)
				{
					var propertyDescription = new PropertyDescription
					{
						DbName = property.Name,
						EntityName = property.Name,
						EntityType = property.PropertyType,
						DbType = property.PropertyType,
						IsRequired = !efType.FindProperty(property.Name).IsNullable,
						IsNew = false
					};
					if (propertiesNodes != null && propertiesNodes.ContainsKey(property.Name))
					{
						var propertyNode = propertiesNodes[property.Name];
						if (propertyNode.Attributes?["entityName"] != null)
						{
							propertyDescription.EntityName = propertyNode.Attributes?["entityName"].Value;
						}
						if (propertyNode.Attributes?["ignore"] != null)
						{
							var ignore = bool.Parse(propertyNode.Attributes?["ignore"].Value);
							if (ignore)
								continue;
						}
						if (propertyNode.Attributes?["isNew"] != null)
						{
							propertyDescription.IsNew = bool.Parse(propertyNode.Attributes?["isNew"].Value);
						}
						if (propertyNode.Attributes?["entityType"] != null)
						{
							var typeName = propertyNode.Attributes?["entityType"].Value;
							var entityPropertyType = Type.GetType(typeName);
							if (entityPropertyType == null)
							{
								MessagesWriter?.WriteLine($"Не удалось загрузить тип {typeName} для свойства {property.Name} сущности {entity.Name}");
							}
							else
							{
								propertyDescription.EntityType = entityPropertyType;
							}
						}
					}
					entity.Properties.Add(propertyDescription);
				}
			}
			return entity;
		}
	}
}
