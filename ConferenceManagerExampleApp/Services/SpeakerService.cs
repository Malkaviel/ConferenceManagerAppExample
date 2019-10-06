using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class SpeakerService:  ISpeakerService
    {
        private readonly ApplicationDbContext _context;

        public SpeakerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SpeakerModel>> GetAllSpeakersAsync()
        {
            return await _context
                .Speaker
                .Include(x => x.SubjectModel)
                .ToListAsync();
        }

        public async Task<bool> AddSpeakerAsync(SpeakerModel speakerModel)
        {
            _context.Speaker.Add(speakerModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveSpeakerAsync(int id)
        {
            var speaker = await _context
                .Speaker
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (speaker == null)
            {
                return false;
            }

            _context.Speaker.Remove(speaker);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }

        public async Task<SpeakerModel> GetSpeakerByIdAsync(int id)
        {
            return await _context
                .Speaker
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}