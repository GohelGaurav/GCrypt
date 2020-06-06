# gcrypt
A .NET Core/Framework library for all your encryption needs.

# Installation
To use gcrypt in your C# project, you can either download the gcrypt C# .NET libraries directly from the Github repository or, if you have the NuGet package manager installed, you can grab them automatically.

```
PM> Install-Package gcrypt
```
Once you have the gcrypt libraries properly referenced in your project, you can include calls to them in your code.

Add the following namespaces to use the library:

```C#
using gcrypt;
using gcrypt.Providers;
```

# Usage
The below code can be used in a .NET project.

```C#
public class Program
{
	public static void Main(string[] args)
	{
		gcrypter.Initialize();

		string originalString = Guid.NewGuid().ToString();

		string encryptedString = gcrypter.Encrypt(originalString);

		string decryptedString = gcrypter.Decrypt(encryptedString);
	}
}
```
By default gcrypt is intialized with a **TripleDESProvider** as 
```C#
new TripleDESProvider( "gcrypt", CipherMode.ECB, PaddingMode.PKCS7 )
```

But you can initialize it with your own set of values :

```C#
gcrypter.Initialize( new TripleDESProvider( "MyOwnKey", CipherMode.CFB, PaddingMode.ANSIX923 ) );
```

##### Stack Multiple Providers :

```C#
gcrypter.Initialize( new TripleDESProvider("MyOwnKey", CipherMode.CFB, PaddingMode.ANSIX923 )
	 , new Base64Provider()
	 , new ReverseProvider(5) );
```

Once initialized **Encrypt / Decrypt** methods will use these providers in their order.

## Create Your Own Providers

You can create a Provider by Implementing **ICryptProvider**.
```C#
public class MyProvider : ICryptProvider
{
	public MyProvider(string config)
	{
		//SOME CODE
	}

	public string Encrypt(string originalString)
	{
		//SOME CODE
	}

	public string Decrypt(string encryptedString)
	{
		//SOME CODE
	}
}
```
And use them something like this :

```C#
gcrypter.Initialize( new MyProvider("SOME CONFIG")
		 , new Base64Provider()
		 , new TripleDESProvider("MyOwnKey", CipherMode.CFB, PaddingMode.ANSIX923)
		 , new ReverseProvider(5));
```

