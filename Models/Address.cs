﻿namespace urlShortener.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string NewUrl { get; set; }
        public int UserId { get; set; }

        public Address(string originalUrl, int userId)
        {
            OriginalUrl = originalUrl;
            UserId = userId;
        }
    }
}
