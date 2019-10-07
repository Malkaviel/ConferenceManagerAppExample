using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface IRoomService
    {
        Task<List<RoomModel>> GetAllRoomsAsync();
        Task<bool> AddRoomAsync(RoomModel roomModel);
        Task<bool> RemoveRoomAsync(int id);

        Task<RoomModel> GetRoomByIdAsync(int id);
    }
}