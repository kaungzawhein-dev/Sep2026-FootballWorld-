namespace Sep2026.FootballWorld.Data
{
    public class Team
    {
        public int Id { get; set; }

        public int LeagueId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public int? FoundedYear { get; set; }

        public string? Stadium { get; set; }

        public string? Manager { get; set; }

        public string? LogoUrl { get; set; }

        public string? Description { get; set; }

        public League? League { get; set; }
    }
}