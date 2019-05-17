namespace ajivaauth.api.Models
{
    public class ContactUsEmailDto
    {
        public string FromAddress { get; set; }
        public string NameOfRecipient { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}