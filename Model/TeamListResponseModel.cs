
namespace FootballWorld.Models
{
    public class TeamListResponseModel
    {
        public List<TeamModel> Data { get; set; }
            = new List<TeamModel>();

        public PaginationModel PageSetting { get; set; }
            = new PaginationModel();
    }
}