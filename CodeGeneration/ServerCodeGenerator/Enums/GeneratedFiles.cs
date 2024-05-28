using System;

namespace CodeGeneration.ServerCodeGenerator.Enums
{
	[Flags]
	internal enum GeneratedFiles : long
	{
		None = 0,
		Entity = 1 << 0,
		SearchParams = 1 << 1,
		Dal = 1 << 2,
		BL = 1 << 3,
		Model = 1 << 4,
		Controller = 1 << 5,
		ViewIndex = 1 << 6,
		ViewUpdate = 1 << 7,
		All = (1 << 8) - 1
	}
}
