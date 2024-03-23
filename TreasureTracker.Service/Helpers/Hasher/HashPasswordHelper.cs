using System.Security.Cryptography;
using System.Text;

namespace TreasureTracker.Service.Helpers.Hasher;
public static class HashPasswordHelper
{
    public static string PasswordHasher(string password)
    {
        password = password.Trim();
        using var sha256 = SHA256.Create();
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltBytes = Encoding.UTF8.GetBytes(Constants.PASSWORD_SALT);
        byte[] bytes = passwordBytes.Concat(saltBytes).ToArray();
        var hashedBytes = sha256.ComputeHash(bytes);
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
    public static bool IsEqual(string curPass, string oldPass)
        => PasswordHasher(curPass) == oldPass;
}
