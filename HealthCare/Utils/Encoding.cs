using Microsoft.IdentityModel.Tokens;

namespace HealthCare.Utils;

public static class Encoding
{
    public static SymmetricSecurityKey SymmetricSecurityKey(string secret)
    {
        return new(System.Text.Encoding.UTF8.GetBytes(secret));
    }
}