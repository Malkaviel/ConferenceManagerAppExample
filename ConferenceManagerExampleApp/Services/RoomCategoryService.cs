using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class RoomCategoryService : IRoomCategoryService
    {
        private readonly ApplicationDbContext _context;

        public RoomCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoomCategoryModel>> GetAllRoomCategoriesAsync()
        {
            return await _context
                .RoomCategory
                .ToListAsync();
        }

        public async Task<bool> AddRoomCategoryAsync(RoomCategoryModel roomCategoryModel)
        {
            _context.RoomCategory.Add(roomCategoryModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveRoomCategoryAsync(int id)
        {
            var roomCategory = await _context
                .RoomCategory
                .Where(category => category.Id == id)
                .SingleOrDefaultAsync();

            if (roomCategory == null)
            {
                return false;
            }

            _context.RoomCategory.Remove(roomCategory);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }

        public async Task<RoomCategoryModel> GetRoomCategoryById(int id)
        {
            return await _context.RoomCategory.Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}