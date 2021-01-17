using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace FiscalNg.Common.Helpers
{
	/// <summary>
	/// Class to encode and decode data.
	/// Some algorithm use <see cref="RijndaelManaged"/>.
	/// Implementation based on: http://www.selamigungor.com/post/7/encrypt-decrypt-a-string-in-csharp.
	/// </summary>
	public static class Cipher
	{
		private static readonly byte[] SaltBytes = { 3, 3, 5, 0, 0, 0, 18, 16 };
		private static readonly byte[] CypherKey = Encoding.UTF8.GetBytes("#c#L1c3nS3S3cr3tKeyFor3NcRyPt10n");

		/// <summary>
		/// Encrypt a string.
		/// </summary>
		/// <param name="plainText">String to be encrypted</param>
		/// <param name="passwordBytes">Password in bytes. Optional. If not set used default key</param>
		public static string Encrypt(string plainText, byte[] passwordBytes = null)
		{
			var bytesEncrypted = EncryptToBytes(plainText, passwordBytes);
			return Convert.ToBase64String(bytesEncrypted);
		}

		/// <summary>
		/// Decrypt a string.
		/// </summary>
		/// <param name="encryptedText">String to be decrypted</param>
		/// <param name="passwordBytes">Password used during encryption. Optional. If not set used default key</param>
		/// <exception cref="FormatException"></exception>
		public static string Decrypt(string encryptedText, byte[] passwordBytes = null)
		{
			var buffer = new Span<byte>(new byte[encryptedText.Length]);
			var success = Convert.TryFromBase64String(encryptedText, buffer, out var bytesWritten);
			if (!success)
				return string.Empty;

			if (passwordBytes == null)
				passwordBytes = CypherKey;
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			var bytesDecrypted = Decrypt(buffer.Slice(0, bytesWritten).ToArray(), passwordBytes);
			return Encoding.UTF8.GetString(bytesDecrypted);
		}

		/// <summary>
		/// Calculates SHA256 checksum
		/// </summary>
		/// <param name="fileBytes">File bytes</param>
		/// <returns>HEX string representation</returns>
		public static string GetSHA256Checksum(byte[] fileBytes)
		{
			using (var sha256 = SHA256.Create())
			{
				var hash = sha256.ComputeHash(fileBytes);
				return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
			}
		}

		/// <summary>
		/// Returns encrypted array of bytes
		/// </summary>
		/// <param name="plainText">String to be encrypted</param>
		/// <param name="passwordBytes">Password in bytes. Optional. If not set used default key</param>
		public static byte[] EncryptToBytes(string plainText, byte[] passwordBytes = null)
		{
			var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);

			if (passwordBytes == null)
				passwordBytes = CypherKey;
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);
			return bytesEncrypted;
		}

		/// <summary>
		/// Creates hashed password from string with random salt
		/// </summary>
		/// <param name="password">Password</param>
		public static (string, byte[]) GetHashString(string password)
		{
			// generate a 128-bit salt using a secure PRNG
			var salt = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			// derive a 512-bit subkey (use HMACSHA1 with 10,000 iterations)
			var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA512,
				iterationCount: 10000,
				numBytesRequested: 512 / 8));

			return (hashed, salt);
		}

		#region Private methods
		/// <summary>
		/// Encrypt bytes by password
		/// </summary>
		/// <param name="bytesToBeEncrypted"></param>
		/// <param name="passwordBytes"></param>
		/// <returns></returns>
		private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
		{
			using (var ms = new MemoryStream())
			using (var aes = new RijndaelManaged())
			{
				var key = new Rfc2898DeriveBytes(passwordBytes, SaltBytes, 2048);

				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Key = key.GetBytes(aes.KeySize / 8);
				aes.IV = key.GetBytes(aes.BlockSize / 8);

				aes.Mode = CipherMode.CBC;

				using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
					cs.Close();
				}

				return ms.ToArray();
			}
		}

		/// <summary>
		/// Decrypt bytes by password
		/// </summary>
		/// <param name="bytesToBeDecrypted"></param>
		/// <param name="passwordBytes"></param>
		/// <returns></returns>
		private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
		{
			using (var ms = new MemoryStream())
			using (var aes = new RijndaelManaged())
			{
				var key = new Rfc2898DeriveBytes(passwordBytes, SaltBytes, 2048);

				aes.KeySize = 256;
				aes.BlockSize = 128;
				aes.Key = key.GetBytes(aes.KeySize / 8);
				aes.IV = key.GetBytes(aes.BlockSize / 8);
				aes.Mode = CipherMode.CBC;

				using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
					cs.Close();
				}

				return ms.ToArray();
			}
		}
		#endregion
	}
}
