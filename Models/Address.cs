namespace urlShortener.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string NewUrl { get; set; }
        public Guid UserId { get; set; }

        public Address(Guid id, string originalUrl, string newUrl, Guid userId)
        {
            Id = id;
            OriginalUrl = originalUrl;
            NewUrl = newUrl;
            UserId = userId;
        }
        public Address(Guid id, string originalUrl, Guid userId)
        {
            Id = id;
            OriginalUrl = originalUrl;
            UserId = userId;
        }
    }
}
