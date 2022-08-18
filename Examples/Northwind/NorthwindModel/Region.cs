namespace NorthwindModel;

public class Region
{
    [NonAutoKey]
    public int RegionID { get; set; }

    [Required]
    public string RegionDescription { get; set; }
}
