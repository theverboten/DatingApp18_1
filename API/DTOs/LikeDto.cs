namespace API.DTOs
{
    public class LikeDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string PhotoUrl {get; set; }
        public string City { get; set; }
        // public string Games { get; set; }
        public string KnownAs { get; set; }
    }
}