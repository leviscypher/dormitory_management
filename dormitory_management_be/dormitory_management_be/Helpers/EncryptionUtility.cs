using System.Security.Cryptography;

namespace dormitory_management_be.Helpers
{
    public class EncryptionUtility
    {
        public static string GenerateRandomKey(int size)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[size];
                rng.GetBytes(key);
                return Convert.ToBase64String(key);
            }
        }

        public static string EncryptString(string plainText, string key, string iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DecryptString(string cipherText, string key, string iv)
        {
            try
            {
                // Ensure that all input strings are trimmed to remove any accidental white space
                cipherText = cipherText.Trim();
                key = key.Trim();
                iv = iv.Trim();

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Convert.FromBase64String(key);
                    aes.IV = Convert.FromBase64String(iv);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Lỗi định dạng (Base64): {ex.Message}");
                Console.WriteLine($"Input data: cipherText={cipherText}, key={key}, iv={iv}");
                throw;
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"Lỗi mã hóa: {ex.Message}");
                throw;
            }
        }


    }
}
