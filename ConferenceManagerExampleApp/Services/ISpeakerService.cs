using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface ISpeakerService
    {
        Task<List<SpeakerModel>> GetAllSpeakersAsync();
        Task<bool> AddSpeakerAsync(SpeakerModel speakerModel);
        Task<bool> RemoveSpeakerAsync(int id);
        Task<SpeakerModel> GetSpeakerByIdAsync(int id);
    }
}