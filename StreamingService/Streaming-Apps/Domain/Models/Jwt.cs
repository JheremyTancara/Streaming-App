using System.Security.Claims;
using Api.Data; 

namespace Api.Models
{
    public class Jwt
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;

        public static dynamic validarToken(ClaimsIdentity identity, DataContext context)
        {
            try
            {
                if (identity.Claims.Count() == 0) 
                {
                    return new
                    {
                        success = false,
                        message = "Verify if you are sending a valid token",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value;
                if (string.IsNullOrEmpty(id)) 
                {
                    return new
                    {
                        success = false,
                        message = "User ID not found in claims",
                        result = ""
                    };
                }

                var usuario = context.Users.FirstOrDefault(x => x.UserID.ToString() == id);
                
                return new
                {
                    success = true,
                    message = "Success",
                    result = usuario
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Exception: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
