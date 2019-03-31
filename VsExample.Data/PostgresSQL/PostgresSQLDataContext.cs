using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VsExample.Data.Entities;

namespace VsExample.Data.PostgresSQL
{
    public class PostgresSQLDataContext: DbContext
    {
        public PostgresSQLDataContext(DbContextOptions<PostgresSQLDataContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<AnimalEntity> Animals { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<FriendShipEntity> FriendShip { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendShipEntity>()
                .HasIndex(friend => friend.FromPerson);
            modelBuilder.Entity<FriendShipEntity>()
                .HasIndex(friend => friend.ToPerson);
            base.OnModelCreating(modelBuilder);
        }
    }
}
