using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VsExample.Data.SQLServerbFirst
{
    public partial class vsexamplesContext : DbContext
    {
        public vsexamplesContext()
        {
        }

        public vsexamplesContext(DbContextOptions<vsexamplesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<FriendShip> FriendShip { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=vsexamples;User ID=SA;Password=rootR0@t;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<FriendShip>(entity =>
            {
                entity.HasIndex(e => e.FromPerson);

                entity.HasIndex(e => e.ToPerson);
            });
        }
    }
}
