namespace Mango.Services.AuthAPI.Models.DTOs
{
    public class ResponsDTO
    {
        public Object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
    }
}
