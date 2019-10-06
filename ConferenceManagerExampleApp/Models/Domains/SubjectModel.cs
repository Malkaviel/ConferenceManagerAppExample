using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.Domains
{
    public class SubjectModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public SubjectCategoryModel SubjectCategoryModel { get; set; }
    }
}