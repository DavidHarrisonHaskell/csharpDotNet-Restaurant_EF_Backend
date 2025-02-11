using System.Security.Cryptography;
using System.Text;

namespace Talent;

public class Cyber
{
    //public static string HashPassword(string plainText)
    //{
    //// hash without salting
    //// SHA: Secure Hashing Algorithm
    //SHA512 sha512 = SHA512.Create();
    //byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
    //byte[] hashBytes = sha512.ComputeHash(plainTextBytes);
    //string hashPassword = Convert.ToBase64String(hashBytes);
    //return hashPassword;

    //}

    public static string HashPassword(string plainText)
    {
        // hash with salting
        // SHA: Secure Hashing Algorithm
        string salt = "Make Things Go Right!"; // needs to be hidden from coders
        byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
        Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainText, saltBytes, 17, HashAlgorithmName.SHA512);
        byte[] hashBytes = rfc.GetBytes(64);
        string hashPassword = Convert.ToBase64String(hashBytes);
        return hashPassword;
    }



}
