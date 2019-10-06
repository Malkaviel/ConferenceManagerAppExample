using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.Domains
{
    public class RoomCategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int MaxCapacity { get; set; }
    }
}