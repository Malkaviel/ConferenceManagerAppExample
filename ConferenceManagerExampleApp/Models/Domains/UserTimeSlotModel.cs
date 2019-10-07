using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ConferenceManagerExampleApp.Models.Domains
{
    public class UserTimeSlotModel
    {
        public int Id { get; set; }
        
        [Required]
        public IdentityUser IdentityUser { get; set; }
        [Required]
        public TimeSlotModel TimeSlotModel { get; set; }
    }
}