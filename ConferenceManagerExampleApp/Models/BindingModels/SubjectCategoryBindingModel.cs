using System.ComponentModel.DataAnnotations;

namespace ConferenceManagerExampleApp.Models.BindingModels
{
    public class SubjectCategoryBindingModel
    {
        [Required]
        public string Description { get; set; }
    }
}