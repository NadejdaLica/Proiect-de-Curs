using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace eUseControl.Helpers
{
    public static class CookieGenerator
    {
        private const string SaltData = "QADLz4qk3rVgBSGjDfAH3XWV" + "qKKagMXezBPv7TmXvwnXDDeR" + "pHaLBv4JnTGRwLg9tzbmV77g" + "8DUEAEa6JPv66hy7SwHBL4z4" + "FbGdh2MVs4kq9RcaZEAszuP5"
                                        + "ccLsEfqCpwdSvVVt479DCZrw" + "jSHrJVwaja9WQaWAmEY9NsPv" + "EHKnFwHTGAvPXpjpCxkbedYq" + "uEauLvZLphwmJLUteZ4QAXU6" + "Z4F3PDmh3wsQXvSctQBHvNWf";
        private static readonly byte[] Salt = Encoding.ASCII.GetBytes(SaltData);

        public static string Create(string value)
        {
            return EncryptStringAes(value, "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP");
        }

        public static string Validate(string value)
        {
            return DecryptStringAes(value, "BjXNmq5MKKaraLwxz9uaATvFwE4Rj679KguTRE8c2j56FnkuKJKfkGbZEeDGFDvsGYNHpUXFUUUuUHBR4UV3T2kumguhubg6Gpt7CyqGDbUPrMvPc67kX3yP");
        }


        /// <summary>
        /// Criptează șirul dat folosind AES.  Șirul poate fi decriptat folosind 
        /// DecryptStringAES().  Parametrii sharedSecret trebuie să se potrivească.
        /// </summary>
        /// <param name="plainText">Textul care urmează să fie criptat.</param>.
        /// <param name="sharedSecret">O parolă folosită pentru a genera o cheie pentru criptare.</param>.
        private static string EncryptStringAes(string plainText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException(nameof(sharedSecret));

            string outStr;                       // Șirul criptat care trebuie returnat
            RijndaelManaged aesAlg = null;              // Obiect RijndaelManaged utilizat pentru a cripta datele.

            try
            {
                // generează cheia din secretul partajat și din sare
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

                // Creează un obiect RijndaelManaged
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

                // Crează un decriptor pentru a efectua transformarea fluxului.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Crează fluxurile utilizate pentru criptare.
                using (var msEncrypt = new MemoryStream())
                {
                    // se adaugă IV
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Scrie toate datele în flux.
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                aesAlg?.Clear();
            }

            // Returnează octeții criptați din fluxul de memorie.
            return outStr;
        }

        /// <summary>
        /// Decriptează șirul dat.  Presupune că șirul a fost criptat folosind 
        /// EncryptStringStringAES(), folosind un sharedSecret identic.
        /// </summary>
        /// <param name="cipherText">Textul care trebuie decriptat.</param>.
        /// <param name="sharedSecret">O parolă folosită pentru a genera o cheie pentru decriptare.</param>.
        private static string DecryptStringAes(string cipherText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException(nameof(sharedSecret));

            // Declarați obiectul RijndaelManaged
            // utilizat pentru decriptarea datelor.
            RijndaelManaged aesAlg = null;

            // Declarați șirul de caractere utilizat pentru a păstra
            // textul decriptat.
            string plaintext;

            try
            {
                // generează cheia din secretul partajat și din sare
                var key = new Rfc2898DeriveBytes(sharedSecret, Salt);

                // Creați fluxurile utilizate pentru decriptare.                
                var bytes = Convert.FromBase64String(cipherText);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    // Creați un obiect RijndaelManaged
                    // cu cheia și IV specificate.
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    // Obține vectorul de inițializare din fluxul criptat
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    // Creați un decriptor pentru a efectua transformarea fluxului.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))

                            // Citiți octeții decriptați din fluxul de decriptare
                            // și plasați-i într-un șir de caractere.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Curățirea RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        private static byte[] ReadByteArray(Stream s)
        {
            var rawLength = new byte[sizeof(int)];
            if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Stream did not contain properly formatted byte array");
            }

            var buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
            if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Did not read byte array properly");
            }

            return buffer;
        }
    }
}