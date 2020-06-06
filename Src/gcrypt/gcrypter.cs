using gcrypt.Providers;
using System;
using System.Runtime.CompilerServices;

namespace gcrypt
{
	public static class gcrypter
	{
		private static ICryptProvider[] _cryptProviders;

		public static bool Initialize(params ICryptProvider[] cryptProviders)
		{
			if (cryptProviders.Length == 0)
				cryptProviders = new ICryptProvider[] { new TripleDESProvider("gcrypt") };

			_cryptProviders = cryptProviders;
			return TestProviders(_cryptProviders);
		}

		public static bool TestProviders(params ICryptProvider[] cryptProviders)
		{
			string failingProviders = "";
			ICryptProvider[] testProviders = cryptProviders;
			if (testProviders.Length == 0)
				testProviders = _cryptProviders;

			//Intividual Test
			foreach (ICryptProvider item in testProviders)
			{
				string originalString = Guid.NewGuid().ToString();
				if (originalString != item.Decrypt(item.Encrypt(originalString)))
				{
					failingProviders += item.GetType().Name + ", ";
				}
			}
			if (!string.IsNullOrEmpty(failingProviders))
			{
				failingProviders = failingProviders.Remove(failingProviders.Length - 2);
				throw new Exception($"Invalid providers, providers {failingProviders} are failing individual tests");
			}

			//Sequential Test
			string originalString2 = Guid.NewGuid().ToString();
			string result = Decrypt(Encrypt(originalString2));

			if (originalString2 == result)
			{
				return true;
			}
			else
			{
				throw new Exception($"Invalid providers, providers are failing sequential test");
			}
		}

		public static string Encrypt(string originalString)
		{
			string value = originalString;
			for (int i = 0; i < _cryptProviders.Length; i++)
			{
				value = _cryptProviders[i].Encrypt(value);
			}
			return value;
		}

		public static string Decrypt(string encryptedString)
		{
			string value = encryptedString;
			for (int i = _cryptProviders.Length - 1; i >= 0; i--)
			{
				value = _cryptProviders[i].Decrypt(value);
			}
			return value;
		}
	}
}
