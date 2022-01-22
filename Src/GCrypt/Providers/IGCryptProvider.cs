namespace GCrypt.Providers
{
	public interface IGCryptProvider
	{
		string Encrypt(string originalString);
		string Decrypt(string encryptedString);
	}
}
