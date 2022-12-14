using Microsoft.EntityFrameworkCore;
using RunGroubweb.Data;
using RunGroubweb.Interfaces;
using RunGroubweb.Models;

namespace RunGroubweb.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Club>> GetAllUserClubs()
        {
            var curUSer = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == curUSer);
            return userClubs.ToList();
        }
        public async Task<List<Race>> GetAllUserRaces()
        {
            var curUSer = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = _context.Races.Where(r => r.AppUser.Id == curUSer);
            return userClubs.ToList();
        }
        public async Task <AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);

        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update (AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
