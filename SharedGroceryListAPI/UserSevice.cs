using Microsoft.AspNetCore.Http.HttpResults;

namespace SharedGroceryListAPI;
using System.IdentityModel.Tokens.Jwt;

public class UserSevice
{
    public string GetAuth0IdFromCookie(string accessToken)
    {
        var jwtToken = new JwtSecurityToken(accessToken);

        string sub = jwtToken.Payload["sub"].ToString();

        if (string.IsNullOrEmpty(sub))
        {
            return new Exception("No string").ToString();
        }
        
        return sub;
    }
}