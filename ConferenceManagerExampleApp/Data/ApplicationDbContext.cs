using System;
using System.Collections.Generic;
using System.Text;
using ConferenceManagerExampleApp.Models.Domains;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManagerExampleApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<RoomCategoryModel> RoomCategory { get; set; }
        public DbSet<RoomModel> Room { get; set; }
        public DbSet<SubjectCategoryModel> SubjectCategory { get; set; }
        public DbSet<SubjectModel> Subject { get; set; }
        public DbSet<SpeakerModel> Speaker { get; set; }
        public DbSet<SessionModel> Session { get; set; }
        
        public DbSet<TimeSlotModel> TimeSlot { get; set; }
        public DbSet<UserTimeSlotModel> UserTimeSlot { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<TimeSlotModel>()
                .HasKey(x => new {x.StartTime, x.EndTime});
        }
    }
}