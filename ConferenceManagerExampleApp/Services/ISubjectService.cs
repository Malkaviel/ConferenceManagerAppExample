using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface ISubjectService
    {
        Task<List<SubjectModel>> GetAllSubjectsAsync();
        Task<bool> AddSubjectAsync(SubjectModel subjectModel);
        Task<bool> RemoveSubjectAsync(int id);
        Task<SubjectModel> GetSubjectByIdAsync(int id);
    }
}