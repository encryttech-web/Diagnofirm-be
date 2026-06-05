using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using SGCrypto;

namespace DiagnofirmAdmin
{
    public class EncryptDecrypt
    {
        public readonly string key;
        public EncryptDecrypt()
        {
            key = Startup.StaticConfig["ApplicationSettings:CryptId"];
        }

        public string Encryptdata(string password)
        {
            byte[] encode = Encoding.UTF8.GetBytes(password);
            string strmsg = Convert.ToBase64String(encode);
            return strmsg;

        }
        public string Decryptdata(string encryptpwd)
        {
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        public string SGEncryption(string encryptStr)
        {
            Encryption encryption = new Encryption(key);
            if (encryptStr != string.Empty)
            {
                string EncryptedStr = encryption.Encrypt(encryptStr).ToString();
                return EncryptedStr;
            }
            else
            {
                string EncryptedStr = encryptStr;
                return EncryptedStr;
            }
        }

        public string SGDecryption(string decryptStr)
        {
            Encryption encryption = new Encryption(key);
            if (decryptStr != string.Empty)
            {
                string DecryptedStr = encryption.Decrypt(decryptStr);
                return DecryptedStr; 
            }
            else
            {
                string DecryptedStr = decryptStr;
                return DecryptedStr;
            }
        }
        public string SGHashing(string passwordStr)
        {
            Hashing hashing = new Hashing(key);
            string HashedStr = hashing.GetHash(passwordStr);
            return HashedStr;
        }

        public string CreateSaltKey()
        {
            string NewSaltKey = Salt.Create();
            return NewSaltKey;
        }

        public bool ValidateHashSaltPswd(string password, string saltKey, string HashedPassword)
        {
            string NewHashPassword = Hash.Create(password, saltKey);
            if (NewHashPassword == HashedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string CreatePasswordWithSalt(string password, string saltKey)
        {
            string NewHashedSaltPassword = Hash.Create(password, saltKey);
            return NewHashedSaltPassword;
        }


    }

    public static class Hash
    {
        public static string Create(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static bool Validate(string value, string salt, string hash)
            => Create(value, salt) == hash;
    }

    public static class Salt
    {
        public static string Create()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
