using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface IRoomCategoryService
    {
        Task<RoomCategoryModel[]> GetAllRoomCategoriesAsync();
    }
}