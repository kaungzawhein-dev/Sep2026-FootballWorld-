
namespace FootballWorld.Models
{
    public class TeamModel
    {
        public int Id { get; set; }

        public int LeagueId { get; set; }

        public string LeagueName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public int? FoundedYear { get; set; }

        public string? Stadium { get; set; }

        public string? Manager { get; set; }

        public string? LogoUrl { get; set; }

        public string? Description { get; set; }
    }
}
