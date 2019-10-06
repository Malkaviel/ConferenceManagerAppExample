using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public class RoomCategoryService : IRoomCategoryService
    {
        public Task<RoomCategoryModel[]> GetAllRoomCategoriesAsync()
        {
            var bigRoomCategory = new RoomCategoryModel
            {
                Description = "A big room",
                MaxCapacity = 300
            };

            var mediumRoomCategory = new RoomCategoryModel
            {
                Description = "A medium room",
                MaxCapacity = 100
            };

            var smallRoomCategory = new RoomCategoryModel
            {
                Description = "A small room",
                MaxCapacity = 20
            };

            return Task.FromResult(new[] {bigRoomCategory, mediumRoomCategory, smallRoomCategory});
        }
    }
}