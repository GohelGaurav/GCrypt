namespace gcrypt.Providers
{
	public interface ICryptProvider
	{
		string Encrypt(string originalString);
		string Decrypt(string encryptedString);
	}
}
