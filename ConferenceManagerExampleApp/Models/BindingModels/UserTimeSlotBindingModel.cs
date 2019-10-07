using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class UserTimeSlotBindingModel
    {
        [Required]
        public string TimeSlotDescription { get; set; }
        
        public List<SelectListItem> TimeSlots { get; set; }
    }
}