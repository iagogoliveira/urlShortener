namespace urlShortener.DTOs
{
    public class CreateNewUrlDto
    {
        public string OriginalUrl { get; set; }
        public int UserId { get; set; }
    }
}
