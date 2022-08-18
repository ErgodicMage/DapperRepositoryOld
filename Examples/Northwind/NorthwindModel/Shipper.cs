namespace NorthwindModel;

[Table("Shippers")]
public class Shipper
{
    [Key]
    public int ShipperID { get; set; }

    [Required]
    public string CompanyName { get; set; }

    public string? Phone { get; set; }
}
