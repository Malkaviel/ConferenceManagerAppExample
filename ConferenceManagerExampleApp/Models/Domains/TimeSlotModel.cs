using System;
using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.Domains
{
    public class TimeSlotModel
    {
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public RoomModel RoomModel { get; set; }
        [Required]
        public SessionModel SessionModel { get; set; }
    }
}