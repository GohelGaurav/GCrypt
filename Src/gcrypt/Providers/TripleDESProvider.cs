using System;
using System.Security.Cryptography;
using System.Text;

namespace GCrypt.Providers
{
	public class TripleDESProviderParams
	{
		public string Key { get; set; }

		public CipherMode Mode { get; set; }
		public PaddingMode Padding { get; set; }
		internal void ConfirmValue()
		{
			if (string.IsNullOrEmpty(Key))
				throw new ArgumentNullException($"{nameof(Key)} can not be null or empty");
		}
	}
	public sealed class TripleDESProvider : TripleDESProviderParams, IGCryptProvider
	{
		internal TripleDESProvider(string key = "gcrypt", CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
		{
			Key = key;
			Mode = mode;
			Padding = padding;
			ConfirmValue();
		}
		public string Encrypt(string originalString)
		{
			var enctArray = UTF8Encoding.UTF8.GetBytes(originalString);
			var objcrpt = new MD5CryptoServiceProvider();
			var srcArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.Key));
			objcrpt.Clear();
			var objt = new TripleDESCryptoServiceProvider {
				Key = srcArray,
				Mode = Mode,
				Padding = Padding
			};
			var crptotrns = objt.CreateEncryptor();
			var resArray = crptotrns.TransformFinalBlock(enctArray, 0, enctArray.Length);
			objt.Clear();
			return Convert.ToBase64String(resArray, 0, resArray.Length);
		}

		public string Decrypt(string encryptedString)
		{
			var decryptArray = Convert.FromBase64String(encryptedString);
			var objmdcript = new MD5CryptoServiceProvider();
			var srcArray = objmdcript.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.Key));
			objmdcript.Clear();
			var objt = new TripleDESCryptoServiceProvider {
				Key = srcArray,
				Mode = Mode,
				Padding = Padding
			};
			var crptotrns = objt.CreateDecryptor();
			var resArray = crptotrns.TransformFinalBlock(decryptArray, 0, decryptArray.Length);
			objt.Clear();
			return UTF8Encoding.UTF8.GetString(resArray);
		}

	}
}
