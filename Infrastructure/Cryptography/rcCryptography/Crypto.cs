using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace rcCryptography
{
    public static class Crypto
    {
        private static string GetKeyMD5(string text)
        {
            return CryptlockMD5(text).Substring(0, 32);
        }

        private static string GetVectorMD5(string text)
        {
            return CryptlockMD5(text).Substring(0, 16);
        }

        private static string GetSaltMD5(string text)
        {
            return CryptlockMD5(text).Substring(0, 16);
        }

        /// <summary>
        /// Misturar o texto com alguns caracteres
        /// </summary>
        /// <param name="text"></param>
        /// <returns>resultado</returns>
        private static string Salt(string text)
        {
            string result = null;
            string saltText = null;

            saltText = GetSaltMD5(text);

            if (!String.IsNullOrEmpty(text))
            {
                int textSize = text.Length;

                for (int i = 0; i < textSize; i++)
                {
                    result += text[i].ToString();

                    if (i < 16)
                    {
                        result += saltText[i].ToString();
                    }
                }
            }
            else
            {
                result = text;
            }

            return result;
        }

        /// <summary>
        /// Retirar caracteres misturados no texto
        /// </summary>
        /// <param name="text"></param>
        /// <returns>resultado</returns>
        private static string Desalt(string text)
        {
            string result = null;

            if (!String.IsNullOrEmpty(text))
            {
                int textSize = text.Length;

                for (int i = 0; i < textSize; i++)
                {
                    if (i < 32)
                    {
                        if (i % 2 == 0)
                        {
                            result += text[i].ToString();
                        }
                    }
                    else
                    {
                        result += text[i].ToString();
                    }
                }
            }
            else
            {
                result = text;
            }

            return result;
        }

        /// <summary>
        /// Transformar texto comum para texto desconhecido descriptografável
        /// </summary>
        /// <returns></returns>
        private static string Encrypt(string password, string key, string vector)
        {
            string secret = null;

            try
            {
                if (password != null)
                {
                    byte[] byteVector = null;
                    byte[] byteKey = null;
                    byte[] textByte = null;

                    MemoryStream memoryStream = null;
                    CryptoStream cryptoStream = null;
                    ICryptoTransform iCryptoTransform = null;
                    Rijndael rijndael = null;

                    byteKey = ASCIIEncoding.ASCII.GetBytes(key);
                    textByte = ASCIIEncoding.ASCII.GetBytes(password);
                    byteVector = ASCIIEncoding.ASCII.GetBytes(vector);

                    rijndael = new RijndaelManaged();
                    rijndael.KeySize = 256;
                    rijndael.Key = byteKey;
                    rijndael.IV = byteVector;
                    rijndael.BlockSize = 128;
                    rijndael.Padding = PaddingMode.PKCS7;

                    iCryptoTransform = rijndael.CreateEncryptor(byteKey, byteVector);

                    memoryStream = new MemoryStream();

                    cryptoStream = new CryptoStream(memoryStream, iCryptoTransform, CryptoStreamMode.Write);
                    cryptoStream.Write(textByte, 0, textByte.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                    memoryStream.Close();

                    textByte = memoryStream.ToArray();

                    secret = Convert.ToBase64String(textByte);
                }
            }
            catch (Exception e)
            {
                secret = null;
            }

            return secret;
        }

        /// <summary>
        /// Transformar texto desconhecido descriptografável para texto comum
        /// </summary>
        /// <returns></returns>
        private static string Decrypt(string password, string key, string vector)
        {
            string secret = null;

            try
            {
                if (password != null)
                {
                    byte[] byteVector = null;
                    byte[] textByte = null;
                    byte[] byteKey = null;

                    MemoryStream memoryStream = null;
                    CryptoStream cryptoStream = null;
                    ICryptoTransform iCryptoTransform = null;
                    Rijndael rijndael = null;

                    byteKey = ASCIIEncoding.ASCII.GetBytes(key);
                    byteVector = ASCIIEncoding.ASCII.GetBytes(vector);

                    rijndael = new RijndaelManaged();
                    rijndael.KeySize = 256;
                    rijndael.Key = byteKey;
                    rijndael.IV = byteVector;
                    rijndael.BlockSize = 128;
                    rijndael.Padding = PaddingMode.PKCS7;

                    iCryptoTransform = rijndael.CreateDecryptor(byteKey, byteVector);

                    memoryStream = new MemoryStream();

                    textByte = Convert.FromBase64String(password);

                    cryptoStream = new CryptoStream(memoryStream, iCryptoTransform, CryptoStreamMode.Write);
                    cryptoStream.Write(textByte, 0, textByte.Length);
                    cryptoStream.FlushFinalBlock();
                    cryptoStream.Close();
                    memoryStream.Close();

                    textByte = memoryStream.ToArray();

                    secret = ASCIIEncoding.ASCII.GetString(textByte);
                }
            }
            catch (Exception e)
            {
                secret = null;
            }

            return secret;
        }

        /// <summary>
        /// Transformar texto comum para texto desconhecido não descriptografável / Hash / MD5
        /// </summary>
        /// <returns></returns>
        private static string CryptlockMD5(string password)
        {
            string secret = null;

            try
            {
                if (!String.IsNullOrEmpty(password))
                {
                    secret = password;

                    byte[] textByte = null;
                    byte[] textoHash = null;

                    StringBuilder sb = null;
                    MD5 md5 = null;

                    md5 = MD5.Create();

                    textByte = ASCIIEncoding.ASCII.GetBytes(secret);

                    textoHash = md5.ComputeHash(textByte);

                    sb = new StringBuilder();

                    for (int i = 0; i < textoHash.Length; i++)
                    {
                        sb.Append(textoHash[i].ToString("X2"));
                    }

                    secret = sb.ToString();
                }
            }
            catch (Exception e)
            {
                secret = null;
            }

            return secret;
        }

        /// <summary>
        /// Transformar texto comum para texto desconhecido não descriptografável / Hash / SHA1
        /// </summary>
        /// <returns></returns>
        private static string CryptlockSHA1(string password)
        {
            string secret = null;

            try
            {
                if (!String.IsNullOrEmpty(password))
                {
                    byte[] textByte = null;
                    byte[] textHash = null;

                    StringBuilder sb = null;
                    SHA1Managed sha1 = null;

                    sha1 = new SHA1Managed();

                    textByte = ASCIIEncoding.ASCII.GetBytes(password);

                    textHash = sha1.ComputeHash(textByte);

                    sb = new StringBuilder();

                    for (int i = 0; i < textHash.Length; i++)
                    {
                        sb.Append(textHash[i].ToString("X2"));
                    }

                    secret = sb.ToString();
                }
            }
            catch (Exception e)
            {
                secret = null;
            }

            return secret;
        }

        /// <summary>
        /// Transformar texto comum para texto desconhecido não descriptografável / Hash / SHA256
        /// </summary>
        /// <returns></returns>
        private static string CryptlockSHA256(string password)
        {
            string secret = null;

            try
            {
                if (!String.IsNullOrEmpty(password))
                {
                    byte[] textByte = null;
                    byte[] textHash = null;

                    StringBuilder sb = null;
                    SHA256Managed sha256 = null;

                    sha256 = new SHA256Managed();

                    textByte = ASCIIEncoding.ASCII.GetBytes(password);

                    textHash = sha256.ComputeHash(textByte);

                    sb = new StringBuilder();

                    for (int i = 0; i < textHash.Length; i++)
                    {
                        sb.Append(textHash[i].ToString("X2"));
                    }

                    secret = sb.ToString();
                }
            }
            catch (Exception e)
            {
                secret = null;
            }

            return secret;
        }

        /// <summary>
        /// Transformar texto comum para texto desconhecido não descriptografável / Hash / SHA512
        /// </summary>
        /// <returns></returns>
        private static string CryptlockSHA512(string password)
        {
            string secret = null;

            try
            {
                if (!String.IsNullOrEmpty(password))
                {
                    byte[] textByte = null;
                    byte[] textHash = null;

                    StringBuilder sb = null;
                    SHA512Managed sha512 = null;

                    sha512 = new SHA512Managed();

                    textByte = ASCIIEncoding.ASCII.GetBytes(password);

                    textHash = sha512.ComputeHash(textByte);

                    sb = new StringBuilder();

                    for (int i = 0; i < textHash.Length; i++)
                    {
                        sb.Append(textHash[i].ToString("X2"));
                    }

                    secret = sb.ToString();
                }
            }
            catch (Exception e)
            {
                secret = null;
            }

            return secret;
        }

        public static string GetSecretSHA512(string original)
        {
            return CryptlockSHA512(original);
        }
    }
}
