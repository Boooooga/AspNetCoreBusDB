using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;

namespace CodeGeneration.ServerCodeGenerator
{
	internal static class Extensions
	{
		internal static string ToFirstLower(this string s)
		{
			return s.Substring(0, 1).ToLowerInvariant() + s.Substring(1);
		}

		internal static string ToFirstUpper(this string s)
		{
			return s.Substring(0, 1).ToUpperInvariant() + s.Substring(1);
		}

		internal static string SplitToLines(this string s, int startingTabsCount, int maxLineWidth, string[] separators, int tabWidth = 4)
		{
			var currentTabsCount = startingTabsCount;
			var stringParts = new List<string>();
			var currentPart = new StringBuilder("");
			for (var i = 0; i < s.Length;)
			{
				var currentSeparator = separators.FirstOrDefault(separator => i + separator.Length <= s.Length 
					&& s.IndexOf(separator, i, separator.Length, StringComparison.Ordinal) == i);
				if (currentSeparator == null)
				{
					currentPart.Append(s[i]);
					++i;
					continue;
				}
				currentPart.Append(currentSeparator);
				stringParts.Add(currentPart.ToString());
				currentPart.Clear();
				i += currentSeparator.Length;
			}
			if (currentPart.Length > 0)
				stringParts.Add(currentPart.ToString());
			var result = new StringBuilder("");
			for (var i = 0; i < stringParts.Count;)
			{
				var currentLine = new StringBuilder(stringParts[i]);
				var j = i + 1;
				while (j < stringParts.Count && stringParts[j].Length + currentLine.Length <= maxLineWidth - currentTabsCount * tabWidth)
				{
					currentLine.Append(stringParts[j]);
					++j;
				}
				result.Append(new string('\t', currentTabsCount));
				result.AppendLine(currentLine.ToString().Trim());
				currentLine.Clear();
				currentTabsCount = startingTabsCount + 1;
				i = j;
			}
			return result.ToString().Substring(0, result.Length - Environment.NewLine.Length);
		}

		internal static string GetFriendlyCSharpName(this Type type, IEnumerable<string> namespacesToRemove = null)
		{
			var underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				return GetFriendlyCSharpName(underlyingType, namespacesToRemove) + "?";
			}
			if (type.IsGenericType)
			{
				var genericArguments = type.GenericTypeArguments.Select(item => GetFriendlyCSharpName(item, namespacesToRemove));
				var genericType = type.GetGenericTypeDefinition();
				var name = genericType.Name;
				var charIndex = name.IndexOf('`');
				if (charIndex >= 0)
					name = name.Substring(0, charIndex);
				return RemoveNamespaces(genericType, name, namespacesToRemove) + "<" + string.Join(", ", genericArguments) + ">";
			}
			using (var provider = new CSharpCodeProvider())
			{
				var result = provider.GetTypeOutput(new CodeTypeReference(type));
				return RemoveNamespaces(type, result, namespacesToRemove);
			}
		}

		private static string RemoveNamespaces(Type type, string typeString, IEnumerable<string> namespacesToRemove)
		{
			var namespacesList = new List<string> { "System" };
			if (namespacesToRemove != null)
				namespacesList.AddRange(namespacesToRemove);
			if (type.Namespace != null && typeString.StartsWith(type.Namespace) && namespacesList.Contains(type.Namespace))
				typeString = typeString.Substring(type.Namespace.Length + 1);
			return typeString;
		}

		internal static bool IsNullable(this Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		internal static Type GetNonNullableType(this Type type)
		{
			var result = Nullable.GetUnderlyingType(type);
			if (result == null)
				result = type;
			return result;
		}
	}
}
