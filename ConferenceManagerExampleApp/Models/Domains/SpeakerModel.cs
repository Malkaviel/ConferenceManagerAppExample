using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.Domains
{
    public class SpeakerModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Title { get; set; }
        
        [Required]
        public SubjectModel SubjectModel { get; set; }
    }
}