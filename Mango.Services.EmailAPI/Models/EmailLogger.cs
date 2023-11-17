namespace Mango.Services.EmailAPI.Models
{
    public class EmailLogger
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string MEssage { get; set; }
        public string Message { get; internal set; }
        public DateTime? EmailSent { get; set; }
    }
}
