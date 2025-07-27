using GCrypt.Providers;
using System;
using System.Runtime.CompilerServices;

namespace GCrypt
{
	public static class GCrypter
	{
		internal static IGCryptProvider[] _cryptProviders { get; set; }

		static GCrypter()
		{
			_cryptProviders = new IGCryptProvider[0];
		}

		internal static bool TestProviders(params IGCryptProvider[] cryptProviders)
		{
			var failingProviders = "";
			var testProviders = cryptProviders;
			if (testProviders.Length == 0)
				testProviders = _cryptProviders;

			//Intividual Test
			foreach (var item in testProviders)
			{
				var originalString = Guid.NewGuid().ToString();
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
			var originalString2 = Guid.NewGuid().ToString();
			var result = Decrypt(Encrypt(originalString2));

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
			var value = originalString;
			for (var i = 0; i < _cryptProviders.Length; i++)
			{
				value = _cryptProviders[i].Encrypt(value);
			}
			return value;
		}

		public static string Decrypt(string encryptedString)
		{
			var value = encryptedString;
			for (var i = _cryptProviders.Length - 1; i >= 0; i--)
			{
				value = _cryptProviders[i].Decrypt(value);
			}
			return value;
		}
	}
}
