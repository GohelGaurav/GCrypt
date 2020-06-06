using System;

namespace gcrypt.Providers
{
	public class ReverseProvider : ICryptProvider
	{
		public sbyte ChunkSize { get; set; }

		public ReverseProvider(sbyte ChunkSize = 0)
		{
			if (ChunkSize < 0)
				throw new Exception("ChunkSize can not be negative.");
			this.ChunkSize = ChunkSize;
		}

		public string Encrypt(string originalString)
		{
			if (ChunkSize == 0)
				return Reverse(originalString);
			else
			{
				string value = "";
				string[] chunks = Split(originalString, ChunkSize);
				for (int i = 0; i < chunks.Length; i++)
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
				string value = "";
				string[] chunks = Split(encryptedString, ChunkSize);
				for (int i = 0; i < chunks.Length; i++)
				{
					value += Reverse(chunks[i]);
				}
				return value;
			}
		}
		private string Reverse(string originalString)
		{
			char[] charArray = originalString.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
		private string[] Split(string originalString, sbyte ChunkSize)
		{
			int chunkCount = originalString.Length / ChunkSize;
			if (originalString.Length % ChunkSize > 0)
				chunkCount++;

			string[] chunks = new string[chunkCount];
			for (int i = 0; i < chunkCount; i++)
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
