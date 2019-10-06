using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class RoomBindingModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int RoomCategoryId { get; set; }
        
        public List<SelectListItem> RoomCategoryDescription { get; set; }
    }
}