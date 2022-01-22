using System;
using System.Collections.Generic;
using GCrypt.Providers;

namespace GCrypt
{
	public sealed class GCryptBuilder : IProviderRegistration
	{
		private static List<IGCryptProvider> _cryptProviders { get; set; }
		private GCryptBuilder()
		{
			_cryptProviders = new List<IGCryptProvider>();
		}

		public static IProviderRegistration Create()
		{
			var builder = new GCryptBuilder();
			return builder;
		}

		public IProviderRegistration Add<T>() where T : IGCryptProvider
		{
			var provider = (T)Activator.CreateInstance(typeof(T));
			_cryptProviders.Add(provider);
			return this;
		}
		public IProviderRegistration Add<T>(Action<T> func) where T : IGCryptProvider
		{
			var provider = (T)Activator.CreateInstance(typeof(T));
			func.Invoke(provider);
			_cryptProviders.Add(provider);
			return this;
		}

		public IProviderRegistration AddBase64()
		{
			var base64Provider = new Base64Provider();
			_cryptProviders.Add(base64Provider);
			return this;
		}

		public IProviderRegistration AddReverse(Action<ReverseProviderParams> func)
		{
			var reverseProvider = new ReverseProvider();
			func.Invoke(reverseProvider);
			reverseProvider.ConfirmValue();
			_cryptProviders.Add(reverseProvider);
			return this;
		}

		public IProviderRegistration AddTripleDES(Action<TripleDESProviderParams> func)
		{
			var tripleDESProvider = new TripleDESProvider();
			func.Invoke(tripleDESProvider);
			tripleDESProvider.ConfirmValue();
			_cryptProviders.Add(tripleDESProvider);
			return this;
		}

		public bool BuildStatic()
		{
			GCrypter._cryptProviders = _cryptProviders.ToArray();
			return GCrypter.TestProviders(GCrypter._cryptProviders);
		}
	}
}
