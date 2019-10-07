using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface ISessionService
    {
        Task<List<SessionModel>> GetAllSessionsAsync();
        Task<bool> AddSessionAsync(SessionModel sessionModel);
        Task<bool> RemoveSessionAsync(int id);
        Task<SessionModel> GetSessionByIdAsync(int id);
    }
}