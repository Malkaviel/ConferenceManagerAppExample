using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class RoomCategoryBindingModel
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int MaxCapacity { get; set; }
    }
}