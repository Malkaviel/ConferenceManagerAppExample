using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.Domains
{
    public class RoomModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public RoomCategoryModel RoomCategoryModel { get; set; }
    }
}