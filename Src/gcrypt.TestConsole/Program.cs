using gcrypt.Providers;
using System;

namespace gcrypt.TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("gcrypter test ");
			try
			{
				bool initResult = gcrypter.Initialize(new ReverseProvider(5), new TripleDESProvider("GG"), new Base64Provider());
				Console.WriteLine($"\ngcrypter.Initialize()\n{initResult}");

				string originalString = Guid.NewGuid().ToString();
				Console.WriteLine($"\nOriginal String\n{originalString}");

				string encryptedString = gcrypter.Encrypt(originalString);
				Console.WriteLine($"\ngcrypter.Encrypt(\"{originalString}\")\n{encryptedString}");

				string decryptedString = gcrypter.Decrypt(encryptedString);
				Console.WriteLine($"\ngcrypter.Decrypt(\"{encryptedString}\")\n{decryptedString}");

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
