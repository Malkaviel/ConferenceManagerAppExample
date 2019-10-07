using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Data;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeSlotModel>> GetAllTimeSlotsAsync()
        {
            return await _context.TimeSlot
                .Include(x => x.SessionModel)
                .ThenInclude(x => x.SpeakerModel)
                .Include(x => x.RoomModel)
                .ToListAsync();
        }

        public async Task<bool> AddTimeSlotAsync(TimeSlotModel timeSlotModel)
        {
            _context.TimeSlot.Add(timeSlotModel);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> RemoveTimeSlotAsync(DateTime startTime, DateTime endTime)
        {
            var timeSlot = await _context
                .TimeSlot
                .Where(x => x.StartTime == startTime && x.EndTime == endTime)
                .SingleOrDefaultAsync();

            if (timeSlot == null)
            {
                return false;
            }

            _context.TimeSlot.Remove(timeSlot);
            var deletionResult = await _context.SaveChangesAsync();
            return deletionResult == 1;
        }

        public async Task<TimeSlotModel> GetTimeSlotByTimeAsync(DateTime startTime, DateTime endTime)
        {
            return await _context
                .TimeSlot
                .Where(x => x.StartTime == startTime && x.EndTime == endTime)
                .SingleOrDefaultAsync();
        }

        public Task<List<DateTime>> GetAllLegalTimeSlotsAsync()
        {
            return Task.FromResult(new List<DateTime>
            {
                new DateTime(2015, 01, 01, 8, 0, 0),
                new DateTime(2015, 01, 01, 10, 0, 0),
                new DateTime(2015, 01, 01, 12, 0, 0),
                new DateTime(2015, 01, 01, 14, 0, 0),
                new DateTime(2015, 01, 01, 16, 0, 0),
                new DateTime(2015, 01, 01, 18, 0, 0),
                
                new DateTime(2015, 01, 02, 8, 0, 0),
                new DateTime(2015, 01, 02, 10, 0, 0),
                new DateTime(2015, 01, 02, 12, 0, 0),
                new DateTime(2015, 01, 02, 14, 0, 0),
                new DateTime(2015, 01, 02, 16, 0, 0),
                new DateTime(2015, 01, 02, 18, 0, 0)
            });
        }

        public Task<bool> AreStartTimeAndEndTimeLegal(DateTime startTime, DateTime endTime)
        {
            var diff = endTime - startTime;
            return Task.FromResult(diff == TimeSpan.FromHours(2));
        }
    }
}