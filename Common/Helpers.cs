using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Troschuetz.Random.Generators;

namespace Common
{
	public static class Helpers
	{
		private static readonly AbstractGenerator RandomGenerator = new StandardGenerator();

		public static string CleanPhone(string phone)
		{
			if (phone == null)
				return null;
			phone = new string(phone.Where(char.IsDigit).ToArray());
			if (phone.Length < 10 || phone.StartsWith("+"))
				return phone;
			if (phone.Length == 10)
				phone = "7" + phone;
			else if (phone.StartsWith("8"))
				phone = "7" + phone.Substring(1);
			return '+' + phone;
		}

		public static string GenerateRandomString(int length = 32, string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789")
		{
			var builder = new StringBuilder();
			for (var i = 0; i < length; ++i)
				builder.Append(alphabet[RandomGenerator.Next(0, alphabet.Length - 1)]);
			return builder.ToString();
		}

		public static string GenerateRandomString(int minLength, int maxLength, string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789")
		{
			return GenerateRandomString(RandomGenerator.Next(minLength, maxLength), alphabet);
		}

		public static int GenerateRandomInt(int minValue, int maxValue)
		{
			return RandomGenerator.Next(minValue, maxValue + 1);
		}

		public static string GetPasswordHash(string s)
		{
			if (s == null)
				return null;
			using var hashAlgorithm = SHA512.Create();
			var hash = hashAlgorithm.ComputeHash(Encoding.Unicode.GetBytes(s));
			return string.Concat(hash.Select(item => item.ToString("x2")));
		}
	}
}
