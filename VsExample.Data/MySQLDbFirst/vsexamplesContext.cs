using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VsExample.Data.MySQLDbFirst
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
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<FriendShip> FriendShip { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=vsexamples;Uid=root;Pwd=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animals>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Age).HasColumnType("int(11)");

                entity.Property(e => e.IsMale).HasColumnType("bit(1)");

                entity.Property(e => e.Name).HasColumnType("longtext");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(95)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<FriendShip>(entity =>
            {
                entity.HasIndex(e => e.FromPerson);

                entity.HasIndex(e => e.ToPerson);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.FromPerson).HasColumnType("int(11)");

                entity.Property(e => e.ToPerson).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Age).HasColumnType("int(11)");

                entity.Property(e => e.IsMale).HasColumnType("bit(1)");

                entity.Property(e => e.Name).HasColumnType("longtext");
            });
        }
    }
}
