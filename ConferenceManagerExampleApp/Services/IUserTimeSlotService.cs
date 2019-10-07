using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.AspNetCore.Identity;

namespace ConferenceManagerExampleApp.Services
{
    public interface IUserTimeSlotService
    {
        Task<List<UserTimeSlotModel>> GetUserTimeSlotsForUserAsync(IdentityUser identityUser);
        Task<bool> AddUserTimeSlotAsync(UserTimeSlotModel userTimeSlotModel);
        Task<bool> RemoveUserTimeSlotAsync(int id);
    }
}