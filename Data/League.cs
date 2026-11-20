namespace Sep2026.FootballWorld.Data
{
    public class League
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        public ICollection<Team> Teams { get; set; }
            = new List<Team>();
    }
}