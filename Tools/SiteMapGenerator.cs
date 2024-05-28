using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tools
{
	public class SiteMapGenerator
	{
		public enum ChangeFrequency
		{
			Always, Hourly, Daily, Weekly, Monthly, Yearly, Never,
		}

		public class SiteMapNode
		{
			public string RelativePath { get; private set; }
			public DateTime ModificationDate { get; private set; }
			public ChangeFrequency ChangeFrequency { get; private set; }
			public double Priority { get; private set; }

			public SiteMapNode(string relativePath, DateTime modificationDate, ChangeFrequency changeFrequency, double priority = 0.5)
			{
				RelativePath = relativePath;
				ModificationDate = modificationDate;
				ChangeFrequency = changeFrequency;
				Priority = priority;
			}
		}

		public string Domain { get; private set; }
		public string Directory { get; private set; }
		public int MaxNodesPerMap { get; private set; }

		public string SiteMapName
		{
			get { return "siteMap.xml"; }
		}

		public string SiteMapPath
		{
			get { return Path.Combine(Directory, SiteMapName); }
		}

		public Encoding SiteMapEncoding
		{
			get { return Encoding.UTF8; }
		}

		private readonly string _dateFormatString = "yyyy-MM-dd";

		public SiteMapGenerator(string domain, string directory, int maxNodesPerMap = 50000)
		{
			if (!domain.EndsWith("/"))
				domain = domain + '/';
			Domain = domain;
			MaxNodesPerMap = maxNodesPerMap;
			Directory = directory;
		}

		public void SaveMap(IEnumerable<SiteMapNode> nodes)
		{
			var writerSettings = new XmlWriterSettings
			{
				Encoding = SiteMapEncoding, 
				Indent = true, 
				IndentChars = "\t"
			};
			var indexWriter = XmlWriter.Create(Path.Combine(Directory, SiteMapName), writerSettings);
			indexWriter.WriteStartDocument();
			indexWriter.WriteStartElement("sitemapindex", "http://www.sitemaps.org/schemas/sitemap/0.9");
			XmlWriter currentWriter = null;
			int nodesCount = 0;
			int mapsCount = 0;
			foreach (var node in nodes)
			{
				if (node != null)
				{
					if (currentWriter == null)
					{
						++mapsCount;
						indexWriter.WriteStartElement("sitemap");
						indexWriter.WriteElementString("loc", string.Format("{0}siteMap{1}.xml", Domain, mapsCount));
						indexWriter.WriteEndElement();
						currentWriter = XmlWriter.Create(Path.Combine(Directory, string.Format("siteMap{0}.xml", mapsCount)), writerSettings);
						currentWriter.WriteStartDocument();
						currentWriter.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
					}
					++nodesCount;
					currentWriter.WriteStartElement("url");
					currentWriter.WriteElementString("loc", Domain + node.RelativePath.TrimStart('/'));
					currentWriter.WriteElementString("lastmod", node.ModificationDate.ToString(_dateFormatString));
					currentWriter.WriteElementString("changefreq", ((Enum)node.ChangeFrequency).ToString().ToLower());
					currentWriter.WriteElementString("priority", node.Priority.ToString(CultureInfo.InvariantCulture));
					currentWriter.WriteEndElement();
					if (nodesCount == MaxNodesPerMap)
					{
						nodesCount = 0;
						currentWriter.WriteEndElement();
						currentWriter.Close();
						currentWriter = null;
					}
				}
			}
			if (currentWriter != null)
			{
				currentWriter.WriteEndElement();
				currentWriter.Close();
			}
			indexWriter.WriteEndElement();
			indexWriter.Close();
		}
	}
}
