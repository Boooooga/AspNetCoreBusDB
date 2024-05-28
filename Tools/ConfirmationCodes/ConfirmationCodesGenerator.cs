using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tools.ConfirmationCodes
{
	public class ConfirmationCodesGenerator
	{
		private SymmetricAlgorithm algorithm;

		private byte[] key;

		private byte[] initVector;

		private long duration;

		private Func<IList<string>, bool> checkFunction;

		private char separator;

		private Encoding encoding;

		public ConfirmationCodesGenerator(SymmetricAlgorithm algorithm, string key, string initVector, long duration, 
			Func<IList<string>, bool> checkFunction, Encoding encoding = null, char separator = '\0')
		{
			this.algorithm = algorithm;
			this.key = Encoding.Unicode.GetBytes(key);
			this.initVector = Encoding.Unicode.GetBytes(initVector);
			if (this.initVector.Length != algorithm.BlockSize / 8)
				throw new Exception($"Initialization vector should consist of {algorithm.BlockSize / 8} bytes");
			if (this.key.Length != algorithm.KeySize / 8)
				throw new Exception($"Key should consist of {algorithm.KeySize / 8} bytes");
			if (duration <= 0)
				throw new Exception("Duration should be positive");
			this.duration = duration;
			this.checkFunction = checkFunction;
			this.separator = separator;
			this.encoding = encoding ?? Encoding.Unicode;
		}

		public string Generate(params string[] values)
		{
			var encryptor = algorithm.CreateEncryptor(key, initVector);
			using (var msEncrypt = new MemoryStream())
			{
				using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					var arr = encoding.GetBytes(string.Join(new string(separator, 1), values) + separator +
						DateTime.Now.Ticks);
					csEncrypt.Write(arr, 0, arr.Length);
				}
				return string.Join("", msEncrypt.ToArray().Select(item => item.ToString("x2")));
			}
		}

		public CodeValidationResult Validate(string code)
		{
			if (string.IsNullOrEmpty(code) || code.Length % 2 == 1)
				return new CodeValidationResult();
			try
			{
				var array = new byte[code.Length / 2];
				for (var i = 0; i < code.Length; i += 2)
					array[i / 2] = byte.Parse(code.Substring(i, 2), NumberStyles.HexNumber);
				var decryptor = algorithm.CreateDecryptor(key, initVector);
				using (var msDecrypt = new MemoryStream(array))
				{
					using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (var srDecrypt = new StreamReader(csDecrypt, encoding))
						{
							var values = srDecrypt.ReadToEnd().Split(separator);
							var time = new DateTime(long.Parse(values.Last()));
							values = values.Take(values.Length - 1).ToArray();
							if (time.AddSeconds(duration) < DateTime.Now)
								return new CodeValidationResult();
							var result = new CodeValidationResult
							{
								ExtractedValues = values,
								IsValid = true
							};
							if (checkFunction != null)
							{
								result.IsValid = checkFunction(values.ToArray());
								if (!result.IsValid)
									result.ExtractedValues = null;
							}
							return result;
						}
					}
				}
			}
			catch
			{
				return new CodeValidationResult();
			}
		}
	}
}
