namespace CompositionDAL.Entities;

[Table("JOBS")]
public class Job
{
    [Key]
    [Column("JOBID")]
    public int JobId { get; set; }

    [Column("JOBSTATUS")]
    public string? Status { get; set; }

    [Column("PRIORITY")]
    public int Priority { get; set; }

    [Column("WORKFLOWID")]
    public string? Workflow { get; set; }

    [Column("REQQUEUE")]
    public string? RequestQueue { get; set; }

    [Column("ORDERID")]
    public int OrderId { get; set; }

    [Column("REFNUM")]
    public string? ReferenceNumber { get; set; }

    [Column("CORPNBR")]
    public string? CorporateNumber { get; set; }

    [Column("SITEID")]
    public string? SiteId { get; set; }

    [Column("ITEMNBR")]
    public string? Item { get; set; }

    [Column("RECORDSYS")]
    public DateTime ReceivedOrderSystem { get; set; }

    [Column("UPDATEORDSYS")]
    public DateTime UpdatedOrderSystem { get; set; }

    [Column("COMPLETEDBY")]
    public DateTime? CompletedDate { get; set; }

    [Column("LASTJOBRUN")]
    public int LastJobRun { get; set; }

    [Column("NOTES")]
    public string? Notes { get; set; }

    [Column("PAGECOUNT")]
    public int PageCount { get; set; }


    public readonly struct JobStatus
    {
        public static readonly string Loading = "Load";
        public static readonly string New = "New";
        public static readonly string Pending = "Pend";
        public static readonly string Scheduled = "Scd";
        public static readonly string Post = "Post";
        public static readonly string Notify = "Note";
        public static readonly string Complete = "Comp";
        public static readonly string Error = "Err";
        public static readonly string Reflow = "Rflw";
    }
}
