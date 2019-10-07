using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class SessionService : ISessionService
    {
        private readonly ApplicationDbContext _context;

        public SessionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SessionModel>> GetAllSessionsAsync()
        {
            return await _context.Session.Include(x => x.SpeakerModel).ToListAsync();
        }

        public async Task<bool> AddSessionAsync(SessionModel sessionModel)
        {
            _context.Session.Add(sessionModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveSessionAsync(int id)
        {
            var session = await _context
                .Session
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (session == null)
            {
                return false;
            }

            _context.Session.Remove(session);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }

        public async Task<SessionModel> GetSessionByIdAsync(int id)
        {
            return await _context
                .Session
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}