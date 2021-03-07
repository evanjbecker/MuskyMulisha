namespace MuskyMulisha.Models
{
    public class EmailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}