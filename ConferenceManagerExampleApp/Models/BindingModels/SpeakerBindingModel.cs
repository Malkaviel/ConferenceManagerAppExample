using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class SpeakerBindingModel
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public int SubjectId { get; set; }
        
        public List<SelectListItem> SubjectTitles { get; set; }
    }
}