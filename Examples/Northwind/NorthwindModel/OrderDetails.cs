namespace NorthwindModel;

public class OrderDetails
{
    [NonAutoKey]
    public int OrderID { get; set; }

    [NonAutoKey]
    public int ProductID { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }

    public float Discount { get; set; }
}
