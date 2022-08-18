namespace NorthwindModel;

public class EmployeeTerritories
{
    [Required]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Required]
    [Column("TerritoryID")]
    public string TerritoryId { get; set; }
}
