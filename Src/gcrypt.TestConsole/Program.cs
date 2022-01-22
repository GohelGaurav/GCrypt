using System;

namespace GCrypt.TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("GCrypt test ");
			try
			{
				var initResult = GCryptBuilder.Create()
					.AddReverse(a => a.ChunkSize = 5)
					.AddTripleDES(a => {
						a.Key = "GG";
						a.Mode = System.Security.Cryptography.CipherMode.ECB;
						a.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
					}).AddBase64()
					.BuildStatic();

				Console.WriteLine($"\ngcrypter.Initialize()\n{initResult}");

				var originalString = Guid.NewGuid().ToString();
				Console.WriteLine($"\nOriginal String\n{originalString}");

				var encryptedString = GCrypt.Encrypt(originalString);
				Console.WriteLine($"\nGCrypt.Encrypt(\"{originalString}\")\n{encryptedString}");

				var decryptedString = GCrypt.Decrypt(encryptedString);
				Console.WriteLine($"\nGCrypt.Decrypt(\"{encryptedString}\")\n{decryptedString}");

				Console.WriteLine($"\nResults match ?\n{string.Equals(originalString, decryptedString)}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"\n\nERROR\n\n{ex}\n\n\n\n");
			}
			Console.Read();
		}
	}
}
