using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConferenceManagerExampleApp.Models.Domains;

namespace ConferenceManagerExampleApp.Services
{
    public interface ITimeSlotService
    {
        Task<List<TimeSlotModel>> GetAllTimeSlotsAsync();
        Task<bool> AddTimeSlotAsync(TimeSlotModel timeSlotModel);
        Task<bool> RemoveTimeSlotAsync(DateTime startTime, DateTime endTime);
        Task<TimeSlotModel> GetTimeSlotByTimeAsync(DateTime startTime, DateTime endTime);

        Task<List<DateTime>> GetAllLegalTimeSlotsAsync();
        Task<bool> AreStartTimeAndEndTimeLegal(DateTime startTime, DateTime endTime);
    }
}