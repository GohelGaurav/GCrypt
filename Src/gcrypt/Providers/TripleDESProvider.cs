using System;
using System.Security.Cryptography;
using System.Text;

namespace gcrypt.Providers
{
	public class TripleDESProvider : ICryptProvider
	{
		public readonly string Key;

		public CipherMode Mode { get; set; }
		public PaddingMode Padding { get; set; }
		public TripleDESProvider(string key, CipherMode mode = CipherMode.ECB, PaddingMode padding = PaddingMode.PKCS7)
		{
			if (string.IsNullOrEmpty(key))
				throw new Exception("Key can not be null or empty");

			this.Key = key;
			this.Mode = mode;
			this.Padding = padding;
		}
		public string Encrypt(string originalString)
		{
			byte[] enctArray = UTF8Encoding.UTF8.GetBytes(originalString);
			MD5CryptoServiceProvider objcrpt = new MD5CryptoServiceProvider();
			byte[] srcArray = objcrpt.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.Key));
			objcrpt.Clear();
			TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider
			{
				Key = srcArray,
				Mode = this.Mode,
				Padding = this.Padding
			};
			ICryptoTransform crptotrns = objt.CreateEncryptor();
			byte[] resArray = crptotrns.TransformFinalBlock(enctArray, 0, enctArray.Length);
			objt.Clear();
			return Convert.ToBase64String(resArray, 0, resArray.Length);
		}

		public string Decrypt(string encryptedString)
		{
			byte[] decryptArray = Convert.FromBase64String(encryptedString);
			MD5CryptoServiceProvider objmdcript = new MD5CryptoServiceProvider();
			byte[] srcArray = objmdcript.ComputeHash(UTF8Encoding.UTF8.GetBytes(this.Key));
			objmdcript.Clear();
			TripleDESCryptoServiceProvider objt = new TripleDESCryptoServiceProvider
			{
				Key = srcArray,
				Mode = this.Mode,
				Padding = this.Padding
			};
			ICryptoTransform crptotrns = objt.CreateDecryptor();
			byte[] resArray = crptotrns.TransformFinalBlock(decryptArray, 0, decryptArray.Length);
			objt.Clear();
			return UTF8Encoding.UTF8.GetString(resArray);
		}

	}
}
