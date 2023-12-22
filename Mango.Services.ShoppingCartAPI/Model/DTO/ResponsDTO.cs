
namespace Mango.Services.ShoppingCartAPI.Model.DTO
{
    using System.Runtime.Serialization;

    public class ResponsDTO
    {
        public Object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
    }
}
