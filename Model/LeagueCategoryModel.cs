
namespace FootballWorld.Models
{
    public class LeagueCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}