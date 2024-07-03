namespace BloggingPlatform.Dto
{
    public class BlogspotDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long NumberOfComments { get; set; } = 0;
    }
}
