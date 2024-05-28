using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeGeneration.ServerCodeGenerator
{
	internal class MicrosoftBuildProject
	{
		public string Path { get; set; }
		private readonly XmlDocument document;
		private Dictionary<string, XmlNode> BuildActionToSectionDictionary { get; set; }

		public MicrosoftBuildProject(string path)
		{
			Path = path;
			document = new XmlDocument();
			document.Load(path);
			BuildActionToSectionDictionary = new Dictionary<string, XmlNode>();
		}

		public void AddItem(Item item)
		{
			var xmlSection = BuildActionToSectionDictionary.ContainsKey(item.BuildAction) ?
				BuildActionToSectionDictionary[item.BuildAction] :
				document.SelectNodes("/Project/ItemGroup").Cast<XmlNode>()
					.OrderByDescending(section => section.SelectNodes(item.BuildAction).Count).FirstOrDefault();
			BuildActionToSectionDictionary[item.BuildAction] = xmlSection;
			if (xmlSection.SelectSingleNode($"{item.BuildAction}[@Include='{item.Path}']") == null)
			{
				var xmlNode = document.CreateNode(XmlNodeType.Element, item.BuildAction, null);
				var includeAttribute = document.CreateAttribute("Include");
				includeAttribute.Value = item.Path;
				xmlNode.Attributes.Append(includeAttribute);
				xmlSection.AppendChild(xmlNode);
			}
		}

		public void Save()
		{
			document.Save(Path);
		}

		public class Item
		{
			public string Path { get; set; }
			public string BuildAction { get; set; }

			public Item(string path, string buildAction)
			{
				Path = path;
				BuildAction = buildAction;
			}
		}
	}
}
