namespace CompositionDAL.Entities;

[Table("JOBS")]
public class Job
{
    [Key]
    [Column("JOBID")]
    public int JobId { get; set; }

    [Column("JOBSTATUS")]
    public string Status { get; set; }

    [Column("PRIORITY")]
    public string Priority { get; set; }

    [Column("WORKFLOWID")]
    public string Workflow { get; set; }

    [Column("REQQUEUE")]
    public string RequestQueue { get; set; }

    [Column("ORDERID")]
    public int OrderId { get; set; }

    [Column("REFNUM")]
    public string ReferenceNumber { get; set; }

    [Column("CUSTNBR")]
    public string Customer { get; set; }

    [Column("ITEMNBR")]
    public string Item { get; set; }

    [Column("RECORDSYS")]
    public DateTime ReceivedOrderSystem { get; set; }

    [Column("UPDATEORDSYS")]
    public DateTime? UpdatedOrderSystem { get; set; }

    [Column("COMPLETEDBY")]
    public DateTime? Completed { get; set; }

    [Column("LASTJOBRUN")]
    public int LastJobRun { get; set; }

    [Column("PAGECOUNT")]
    public string PageCount { get; set; }


}
