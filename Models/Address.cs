namespace urlShortener.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string NewUrl { get; set; } = string.Empty;
        public int UserId { get; set; }

    }
}
