using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface ISubjectCategoryService
    {
        Task<List<SubjectCategoryModel>> GetAllSubjectCategoryAsync();
        Task<bool> AddSubjectCategoryAsync(SubjectCategoryModel subjectCategoryModel);
        Task<bool> RemoveSubjectCategoryAsync(int id);
        Task<SubjectCategoryModel> GetSubjectCategoryById(int id);
    }
}