using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class UserTimeSlotService : IUserTimeSlotService
    {
        private readonly ApplicationDbContext _context;

        public UserTimeSlotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserTimeSlotModel>> GetUserTimeSlotsForUserAsync(IdentityUser identityUser)
        {
            return await _context
                .UserTimeSlot
                .Include(x => x.TimeSlotModel)
                .ThenInclude(x => x.RoomModel)
                .Include(x => x.TimeSlotModel)
                .ThenInclude(x => x.SessionModel)
                .ThenInclude(x => x.SpeakerModel)
                .Where(x => x.IdentityUser == identityUser)
                .ToListAsync();
        }

        public async Task<bool> AddUserTimeSlotAsync(UserTimeSlotModel userTimeSlotModel)
        {
            _context.UserTimeSlot.Add(userTimeSlotModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveUserTimeSlotAsync(int id)
        {
            var userTimeSlot = await _context
                .UserTimeSlot
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (userTimeSlot == null)
            {
                return false;
            }

            _context.UserTimeSlot.Remove(userTimeSlot);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }
    }
}