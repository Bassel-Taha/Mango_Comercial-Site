
public class CartDetailsDto
{
    public int CartDetailsID { get; set; }

    public int CartHeaderID { get; set; }

    public virtual CartHeaderDto? CartHeader { get; set; }

    public int ProductID { get; set; }

    public ProductsDto? Product { get; set; }

    public int Count { get; set; }
}