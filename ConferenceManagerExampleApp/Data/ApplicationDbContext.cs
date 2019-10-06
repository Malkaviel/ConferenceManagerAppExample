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
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}