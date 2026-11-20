
namespace FootballWorld.Models
{
    public class LeagueListResponseModel
    {
        public List<LeagueModel> Data { get; set; }
            = new List<LeagueModel>();

        public PaginationModel PageSetting { get; set; }
            = new PaginationModel();
    }
}