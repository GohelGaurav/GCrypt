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
using GCrypt;
```

# Usage
The below code can be used in any .NET C# project.

```C#
public class Program
{
	public static void Main(string[] args)
	{
		//Once In Program.cs Or Startup.cs
		GCryptBuilder.Create()
			.AddTripleDES(a => {
				a.Key = "gcrypt";
				a.Mode = CipherMode.ECB;
				a.Padding = PaddingMode.PKCS7;
			})
			.BuildStatic();

		string originalString = Guid.NewGuid().ToString();

		string encryptedString = GCrypt.Encrypt(originalString);

		string decryptedString = GCrypt.Decrypt(encryptedString);
	}
}
```


##### Stack Multiple Providers :

```C#
GCryptBuilder.Create()
	.AddReverse(a => a.ChunkSize = 5)
	.AddTripleDES(a => {
		a.Key = "gcrypt";
		a.Mode = CipherMode.ECB;
		a.Padding = PaddingMode.PKCS7;
	})
	.AddBase64()
	.BuildStatic();
```

Once initialized **Encrypt / Decrypt** methods will use these providers in their order.

## Create Your Own Providers

You can create a Provider by Implementing **IGCryptProvider**.
```C#
public class MyProvider : IGCryptProvider
{
	public string Config { get; set; }

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
GCryptBuilder.Create()
	.AddReverse(a => a.ChunkSize = 5)
	.Add<MyProvider>(a => a.Config = "Some Config")
	.BuildStatic();
```

