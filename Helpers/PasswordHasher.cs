using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace api_cine_search.Helpers
{
  public class PasswordHasher
  {

    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;

    public static (byte[] salt, int saltSize, string hashedPassword) HashPassword(string password)
    {
      // Generate a random salt size between 16 and 32 bytes

      Random rand = new Random();
      int saltSize = rand.Next(16, 33);

      using (var algorithm = new Rfc2898DeriveBytes(password, saltSize, Iterations, HashAlgorithmName.SHA256))
      {
        var salt = algorithm.Salt;
        var key = algorithm.GetBytes(KeySize);

        var saltAndPasswordHash = new byte[saltSize + KeySize];
        Array.Copy(salt, 0, saltAndPasswordHash, 0, saltSize);
        Array.Copy(key, 0, saltAndPasswordHash, saltSize, KeySize);

        return (salt, saltSize, Convert.ToBase64String(saltAndPasswordHash));
      }
    }

    public static bool VerifyPassword(string hashedPassword, string password, byte[] salt, int saltSize)
    {
      var saltAndPasswordHash = Convert.FromBase64String(hashedPassword);

      var key = new byte[KeySize];
      Array.Copy(saltAndPasswordHash, saltSize, key, 0, KeySize);

      using (var algorithm = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
      {
        var keyToCheck = algorithm.GetBytes(KeySize);
        return keyToCheck.SequenceEqual(key);
      }
    }
  }
}