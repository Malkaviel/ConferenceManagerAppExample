using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class SubjectBindingModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int SubjectCategoryId { get; set; }
        public List<SelectListItem> SubjectCategoryDescription { get; set; }
    }
}