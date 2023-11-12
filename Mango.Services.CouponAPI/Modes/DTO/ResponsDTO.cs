namespace Mango.Services.CouponAPI.Modes.DTO
{
    public class ResponsDTO
    {
        public Object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
    }
}
