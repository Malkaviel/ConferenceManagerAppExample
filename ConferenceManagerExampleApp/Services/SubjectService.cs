using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext _context;

        public SubjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubjectModel>> GetAllSubjectsAsync()
        {
            return await _context
                .Subject
                .Include(x => x.SubjectCategoryModel)
                .ToListAsync();
        }

        public async Task<bool> AddSubjectAsync(SubjectModel subjectModel)
        {
            _context.Subject.Add(subjectModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveSubjectAsync(int id)
        {
            var subject = await _context
                .Subject
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (subject == null)
            {
                return false;
            }

            _context.Subject.Remove(subject);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }
    }
}