using System.Security.Cryptography;
using System.Text;

namespace BankApp; 

public static class Utils {
    
    public static string HashPassword(string input) {
        var hashSalty = "bZ65Y!Xf%ezdg#";
        var inputBytes = Encoding.UTF8.GetBytes(input + hashSalty);
        var inputHash = SHA256.HashData(inputBytes);
        return Convert.ToHexString(inputHash);
    }
    
}