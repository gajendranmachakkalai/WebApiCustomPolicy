using Microsoft.EntityFrameworkCore;
using MTAPP.DAL.Model;
using MTAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTAPP.DAL
{
    public partial class MTAppContext : DbContext
    {
        public MTAppContext(DbContextOptions<MTAppContext> options): base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<AppUser> AppUser { get; set; }
        public virtual DbSet<AppRole> AppRole { get; set; }
        public virtual DbSet<AppRoute> AppRoute { get; set; }
        public virtual DbSet<AppRoleRoute> AppRoleRoute { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }


    }
}
