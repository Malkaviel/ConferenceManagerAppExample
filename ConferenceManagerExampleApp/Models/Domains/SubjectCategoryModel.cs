using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.Domains
{
    public class SubjectCategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}