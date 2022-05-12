namespace CompositionDAL.Entities;

[Table("ORDLINEINFO")]
public class OrderLine
{
    [Column("JOBID")]
    public int JobId { get; set; }

    [Column("SITEID")]
    public int SiteId { get; set; }

    [Column("SITENAME")]
    public string SiteName { get; set; }

    [Column("SHPNAME")]
    public string? ShipName { get; set; }

    [Column("SHPADDR1")]
    public string? ShipAddress1 { get; set; }

    [Column("SHPADDR2")]
    public string? ShipAddress2 { get; set; }

    [Column("SHPCITY")]
    public string? ShipCity { get; set; }

    [Column("SHPSTATE")]
    public string? ShipState { get; set; }

    [Column("SHPCOUNTRY")]
    public string? ShipCountry { get; set; }

    [Column("SHPZIP")]
    public string? ShipZipCode { get; set; }

    [Column("SHPPHONE")]
    public string? ShipPhoneNumber { get; set; }

    [Column("LINENBR")]
    public int LineNumber { get; set; }

    [Column("ORDLINEQTY")]
    public int OrderQuantity { get; set; }

    [Column("PROJECTID")]
    public int ProjectId { get; set; }

    [Column("PROJECTFLDRURL")]
    public string? ProjectFolder { get; set; }

    [Column("PROJECTFILENAME")]
    public string? ProjectFileName { get; set; }

    [Column("PCNAME")]
    public string? ProjectContactName { get; set; }

    [Column("PCPHONE")]
    public string? ProjectContactPhoneNumber { get; set; }

    [Column("PCEMAIL")]
    public string? ProjectContactEmail { get; set; }

}
