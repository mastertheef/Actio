using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Domain.Services
{
    public class Encriptor : IEncriptor
    {
        private static readonly int SaltSize = 40;
        private static readonly int DeriveBytesIterationsCount = 10000;

        public string GetHash(string value, string salt)
        {
            throw new NotImplementedException();
        }

        public string GetSalt(string value)
        {
            throw new NotImplementedException();
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
