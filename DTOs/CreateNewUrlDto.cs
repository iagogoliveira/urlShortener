namespace urlShortener.DTOs
{
    public class CreateNewUrlDto
    {
        public string OriginalUrl { get; set; }
        public string NewUrl { get; set; }
        public int UserId { get; set; }
    }
}
