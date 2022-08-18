namespace NorthwindModel;

public class Territory
{
    [Key]
    [Column("TerritoryID", 20)]
    public string TerritoryId { get; set; }

    [Required]
    public string TerritoryDescription { get; set; }

    [Required]
    [Column("RegionID")]
    public int RegionId { get; set; }
}
