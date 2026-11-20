namespace FootballWorld.Models
{
    public class LeagueModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }
    }
}