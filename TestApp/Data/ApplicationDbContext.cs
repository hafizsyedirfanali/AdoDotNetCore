using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PersonalDetails> PersonalDetails { get; set; }
    }
    public class PersonalDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
}
