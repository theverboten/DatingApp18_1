namespace API.Helpers
{
    public class MessageParams : PagiantionParams
    {
        public string Username { get; set; }
        public string Container { get; set; } = "Unread";
        
    }
}