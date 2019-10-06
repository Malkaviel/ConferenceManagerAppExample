using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomModel>> GetAllRoomsAsync()
        {
            return await _context.Room.Include(room => room.RoomCategoryModel).ToListAsync();
        }

        public async Task<bool> AddRoomAsync(RoomModel roomModel)
        {
            _context.Room.Add(roomModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveRoomAsync(int id)
        {
            var room = await _context
                .Room
                .Where(ro => ro.Id == id)
                .SingleOrDefaultAsync();

            if (room == null)
            {
                return false;
            }

            _context.Room.Remove(room);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }
    }
}