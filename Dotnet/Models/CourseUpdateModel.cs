namespace PurposeBuddy.Models
{
    public class CourseUpdateModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double? Price { get; set; }
        public bool? IsPublished { get; set; }
        public string? CategoryId { get; set; }
    }
}
