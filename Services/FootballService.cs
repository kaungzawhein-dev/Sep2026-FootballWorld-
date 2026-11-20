using Sep2026.FootballWorld.Data;
using FootballWorld.Models;
using Microsoft.EntityFrameworkCore;
using Sep2026.FootballWorld.Database.AppDbContextModels;

namespace FootballWorld.Services
{
    public class FootballService
    {
        private readonly FootballDbContext _context;

        public FootballService()
        {
            _context = new FootballDbContext();
        }

        // GET ALL LEAGUES WITH PAGINATION
        public async Task<LeagueListResponseModel> GetDataByPagination(
            int pageNo = 1,
            int pageSize = 10,
            string? category = null)
        {
            pageNo = Math.Max(1, pageNo);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _context.Leagues.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(x =>
                    x.Country.Contains(category) ||
                    x.Name.Contains(category));
            }

            int totalCount = await query.CountAsync();

            int pageCount = (int)Math.Ceiling(
                totalCount / (double)pageSize);

            var data = await query
                .OrderBy(x => x.Name)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new LeagueModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Country = x.Country,
                    LogoUrl = x.LogoUrl
                })
                .ToListAsync();

            return new LeagueListResponseModel
            {
                Data = data,
                PageSetting = new PaginationModel
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    PageCount = pageCount
                }
            };
        }

        // GET LEAGUE CATEGORIES
        public async Task<LeagueCategoryResponseModel> GetCategoryList()
        {
            var data = await _context.Leagues
                .AsNoTracking()
                .Select(league => new LeagueCategoryModel
                {
                    Id = league.Id,
                    Name = league.Name,
                    Country = league.Country,
                    Count = league.Teams.Count()
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            return new LeagueCategoryResponseModel
            {
                Data = data
            };
        }

        // GET LEAGUE BY ID
        public async Task<LeagueModel?> GetLeagueById(int id)
        {
            return await _context.Leagues
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new LeagueModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Country = x.Country,
                    LogoUrl = x.LogoUrl
                })
                .FirstOrDefaultAsync();
        }

        // GET ALL TEAMS WITH PAGINATION
        public async Task<TeamListResponseModel> GetAllTeams(
            int pageNo = 1,
            int pageSize = 10,
            string? category = null)
        {
            pageNo = Math.Max(1, pageNo);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _context.Teams.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(x =>
                    x.Name.Contains(category) ||
                    x.Country.Contains(category));
            }

            int totalCount = await query.CountAsync();

            int pageCount = (int)Math.Ceiling(
                totalCount / (double)pageSize);

            var data = await query
                .OrderBy(x => x.Name)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TeamModel
                {
                    Id = x.Id,
                    LeagueId = x.LeagueId,
                    LeagueName = x.League != null
                        ? x.League.Name : string.Empty,
                    Name = x.Name,
                    Country = x.Country,
                    FoundedYear = x.FoundedYear,
                    Stadium = x.Stadium,
                    Manager = x.Manager,
                    LogoUrl = x.LogoUrl,
                    Description = x.Description
                })
                .ToListAsync();

            return new TeamListResponseModel
            {
                Data = data,
                PageSetting = new PaginationModel
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    PageCount = pageCount
                }
            };
        }

        // GET TEAMS BY LEAGUE ID
        public async Task<TeamListResponseModel> GetTeamsByLeague(
            int leagueId,
            int pageNo = 1,
            int pageSize = 10)
        {
            pageNo = Math.Max(1, pageNo);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _context.Teams
                .AsNoTracking()
                .Where(x => x.LeagueId == leagueId);

            int totalCount = await query.CountAsync();

            int pageCount = (int)Math.Ceiling(
                totalCount / (double)pageSize);

            var data = await query
                .OrderBy(x => x.Name)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new TeamModel
                {
                    Id = x.Id,
                    LeagueId = x.LeagueId,
                    LeagueName = x.League != null
                        ? x.League.Name : string.Empty,
                    Name = x.Name,
                    Country = x.Country,
                    FoundedYear = x.FoundedYear,
                    Stadium = x.Stadium,
                    Manager = x.Manager,
                    LogoUrl = x.LogoUrl,
                    Description = x.Description
                })
                .ToListAsync();

            return new TeamListResponseModel
            {
                Data = data,
                PageSetting = new PaginationModel
                {
                    PageNo = pageNo,
                    PageSize = pageSize,
                    PageCount = pageCount
                }
            };
        }

        // GET TEAM BY ID
        public async Task<TeamModel?> GetTeamById(int id)
        {
            return await _context.Teams
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new TeamModel
                {
                    Id = x.Id,
                    LeagueId = x.LeagueId,
                    LeagueName = x.League != null
                        ? x.League.Name : string.Empty,
                    Name = x.Name,
                    Country = x.Country,
                    FoundedYear = x.FoundedYear,
                    Stadium = x.Stadium,
                    Manager = x.Manager,
                    LogoUrl = x.LogoUrl,
                    Description = x.Description
                })
                .FirstOrDefaultAsync();
        }
    }
}
