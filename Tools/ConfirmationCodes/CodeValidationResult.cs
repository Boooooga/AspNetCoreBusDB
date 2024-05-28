using System.Collections.Generic;

namespace Tools.ConfirmationCodes
{
	public class CodeValidationResult
	{
		public bool IsValid { get; set; }
		public IList<string> ExtractedValues { get; set; }
	}
}
