namespace Api.Models

{
    public class TokenValidationResult
    {
        public bool Success { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
    }
}
