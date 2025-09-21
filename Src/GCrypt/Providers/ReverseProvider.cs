using System;

namespace GCrypt.Providers
{
	public class ReverseProviderParams
	{
		public sbyte ChunkSize { get; set; }
		internal void ConfirmValue()
		{
			if (ChunkSize < 0)
				throw new ArgumentOutOfRangeException($"{nameof(ChunkSize)} can not be negative.");
		}
	}
	public sealed class ReverseProvider : ReverseProviderParams, IGCryptProvider
	{
		internal ReverseProvider(sbyte chunkSize = 0)
		{
			ChunkSize = chunkSize;
			ConfirmValue();
		}

		public string Encrypt(string originalString)
		{
			if (ChunkSize == 0)
				return Reverse(originalString);
			else
			{
				var value = "";
				var chunks = Split(originalString, ChunkSize);
				for (var i = 0; i < chunks.Length; i++)
				{
					value += Reverse(chunks[i]);
				}
				return value;
			}
		}

		public string Decrypt(string encryptedString)
		{
			if (ChunkSize == 0)
				return Reverse(encryptedString);
			else
			{
				var value = "";
				var chunks = Split(encryptedString, ChunkSize);
				for (var i = 0; i < chunks.Length; i++)
				{
					value += Reverse(chunks[i]);
				}
				return value;
			}
		}
		internal string Reverse(string originalString)
		{
			var charArray = originalString.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
		private string[] Split(string originalString, sbyte ChunkSize)
		{
			var chunkCount = originalString.Length / ChunkSize;
			if (originalString.Length % ChunkSize > 0)
				chunkCount++;

			var chunks = new string[chunkCount];
			for (var i = 0; i < chunkCount; i++)
			{
				if (i == chunkCount - 1)
					chunks[i] = originalString.Substring(i * ChunkSize);
				else
					chunks[i] = originalString.Substring(i * ChunkSize, ChunkSize);
			}
			return chunks;
		}
	}
}
