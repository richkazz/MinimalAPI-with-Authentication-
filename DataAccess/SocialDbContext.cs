using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SocialDbContext : IdentityDbContext
    {
        public SocialDbContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Post> Posts { get; set; }
        public DbSet<ActiveSchoolTerm> ActiveSchoolTerms { get; set; }
    }
}
