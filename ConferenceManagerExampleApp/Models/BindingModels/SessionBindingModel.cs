using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class SessionBindingModel
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public int SpeakerId { get; set; }
        
        public List<SelectListItem> SpeakerNames { get; set; }
    }
}