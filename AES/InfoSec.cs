using System.Security.Cryptography;

namespace AES
{
    //code to generate the AES key
    public static class InfoSec
    {
        public static string GenerateKey()
        {
            string keyBase64 = "";

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();

                keyBase64 = Convert.ToBase64String(aes.Key);
            }

            return keyBase64;
        }

        public static string Encrypt(string plainText, string key, out string ivKey)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.Zeros;
                aes.Key = Convert.FromBase64String(key);
                aes.GenerateIV();

                ivKey = Convert.ToBase64String(aes.IV);

                ICryptoTransform encryptor = aes.CreateEncryptor();

                byte[] encryptedData;


                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }

                        encryptedData = ms.ToArray();
                    }
                }
                return Convert.ToBase64String(encryptedData);
            }
        }
        public static string Decrypt(string cipher, string key, string ivKey)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.Zeros;
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(ivKey);



                ICryptoTransform decryptor = aes.CreateDecryptor();

                string plainText = "";
                byte[] cipherText = Convert.FromBase64String(cipher);

                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            plainText = sr.ReadToEnd();
                        }
                    }
                }
                return plainText.ToString().TrimEnd(new char[] { '\0' });
            }
        }
    }
}
