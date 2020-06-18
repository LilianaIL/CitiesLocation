using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CitiesLocation.Models
{
    [Table("users")]
    public class User
    {

        private string userId;

        [Key]
        public string UserId
        {
            get
            { return userId; }
            set
            { userId = GenerateUniqueID(); }
        }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [NotMapped]
        public string Token { get; set; }


        private static readonly RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        private const int DefaultIDLength = 11;
        private static string GenerateUniqueID(int length = DefaultIDLength)
        {
            int sufficientBufferSizeInBytes = (length * 6 + 7) / 8;
            var buffer = new byte[sufficientBufferSizeInBytes];
            random.GetBytes(buffer);
            return Convert.ToBase64String(buffer).Substring(0, length);
        }

        public void GetUniqueToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            Token = token;            
        }

        


    }
}
