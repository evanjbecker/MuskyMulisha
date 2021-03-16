namespace MuskyMulisha.Models
{
    public class EmailStatus
    {
        public EmailStatusEnum StatusEnum { get; set; }
        public string Message { get; set; }
    }

    public enum EmailStatusEnum
    {
        Error,
        Success
    }
}