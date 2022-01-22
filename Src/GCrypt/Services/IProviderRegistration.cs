using System;
using GCrypt.Providers;

namespace GCrypt
{
	public interface IProviderRegistration
	{
		public IProviderRegistration AddTripleDES(Action<TripleDESProviderParams> func);
		public IProviderRegistration AddBase64();
		public IProviderRegistration AddReverse(Action<ReverseProviderParams> func);
		public IProviderRegistration Add<T>() where T : IGCryptProvider;
		public IProviderRegistration Add<T>(Action<T> func) where T : IGCryptProvider;
		public bool BuildStatic();
	}
}
