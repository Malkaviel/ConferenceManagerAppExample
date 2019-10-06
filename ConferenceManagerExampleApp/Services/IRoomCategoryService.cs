using System.Collections.Generic;
using System.Threading.Tasks;

using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface IRoomCategoryService
    {
        Task<List<RoomCategoryModel>> GetAllRoomCategoriesAsync();
        Task<bool> AddRoomCategoryAsync(RoomCategoryModel roomCategoryModel);
        Task<bool> RemoveRoomCategoryAsync(int id);
        Task<RoomCategoryModel> GetRoomCategoryById(int id);
    }
}