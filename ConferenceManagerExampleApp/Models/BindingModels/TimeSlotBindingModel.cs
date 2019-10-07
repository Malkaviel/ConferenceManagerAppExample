using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class TimeSlotBindingModel
    {
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int SessionId { get; set; }
        
        public List<SelectListItem> TimeSlots { get; set; }
        public List<SelectListItem> RoomNames { get; set; }
        public List<SelectListItem> SessionTitles { get; set; }
    }
}