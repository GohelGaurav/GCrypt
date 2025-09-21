using System;
using System.Text;

namespace GCrypt.Providers
{
	public sealed class Base64Provider : IGCryptProvider
	{
		public string Encrypt(string originalString)
		{
			return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(originalString));
		}

		public string Decrypt(string encryptedString)
		{
			return ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(encryptedString));
		}

	}
}
