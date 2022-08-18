namespace NorthwindModel;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    public string ProductName { get; set; }

    public int? SupplierID { get; set; }

    public int? CategoryID { get; set; }

    public string? QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? RecorderLevel { get; set; }

    public bool? Discontinued { get; set; }
}
