using System;
using System.Collections.Generic;
using System.Linq;
using CodeGeneration.ServerCodeGenerator.Enums;

namespace CodeGeneration.ServerCodeGenerator
{
	internal class PropertyDescription
	{
		internal string DbName { get; set; }
		internal string EntityName { get; set; }
		internal Type DbType { get; set; }
		internal Type EntityType { get; set; }
		internal bool IsRequired { get; set; }
		internal bool IsNew { get; set; }

		internal bool RepresentsBitMask()
		{
			var nonNullableType = DbType.GetNonNullableType();
			if ((nonNullableType == typeof(int) || nonNullableType == typeof(long)) && EntityType.IsGenericType &&
			    EntityType.GetGenericTypeDefinition() == typeof(List<>))
			{
				var genericArgument = EntityType.GetGenericArguments().FirstOrDefault();
				return genericArgument != null && genericArgument.IsEnum && genericArgument.GetEnumUnderlyingType() == nonNullableType;
			}
			return false;
		}

		internal PropertyDisplayType GetDisplayType()
		{
			if (EntityName.Equals("Id", StringComparison.InvariantCulture))
				return PropertyDisplayType.Hidden;
			if (EntityType == typeof(bool))
				return PropertyDisplayType.Bool;
			if (EntityType == typeof(bool?))
				return PropertyDisplayType.NullableBool;
			if (EntityType.IsEnum)
				return PropertyDisplayType.Enum;
			if (Nullable.GetUnderlyingType(EntityType)?.IsEnum == true)
				return PropertyDisplayType.NullableEnum;
			if (RepresentsBitMask())
				return PropertyDisplayType.BitMask;
			if (EntityType == typeof(DateTime) || EntityType == typeof(DateTime?))
				return PropertyDisplayType.Date;
			return PropertyDisplayType.Default;
		}
	}
}
