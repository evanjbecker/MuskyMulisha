namespace MuskyMulisha.Models
{
    public class EmailStatus
    {
        public ErrorStatus Status { get; set; }
        public string Message { get; set; }
    }

    public enum ErrorStatus
    {
        Error,
        Success
    }
}