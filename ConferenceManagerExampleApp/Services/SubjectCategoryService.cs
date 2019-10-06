using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class SubjectCategoryService : ISubjectCategoryService
    {
        private readonly ApplicationDbContext _context;

        public SubjectCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubjectCategoryModel>> GetAllSubjectCategoryAsync()
        {
            return await _context.SubjectCategory.ToListAsync();
        }

        public async Task<bool> AddSubjectCategoryAsync(SubjectCategoryModel subjectCategoryModel)
        {
            _context.SubjectCategory.Add(subjectCategoryModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveSubjectCategoryAsync(int id)
        {
            var subjectCategory = await _context
                .SubjectCategory
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (subjectCategory == null)
            {
                return false;
            }

            _context.SubjectCategory.Remove(subjectCategory);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }

        public async Task<SubjectCategoryModel> GetSubjectCategoryById(int id)
        {
            return await _context
                .SubjectCategory
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}