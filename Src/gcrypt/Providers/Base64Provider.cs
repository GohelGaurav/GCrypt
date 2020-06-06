using System;
using System.Text;

namespace gcrypt.Providers
{
	public class Base64Provider : ICryptProvider
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
