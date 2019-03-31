using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using VsExample.Data.Entities;

namespace VsExample.Data.MySQL
{
    public class MySQLDataContext: DbContext
    {
        public MySQLDataContext(DbContextOptions<MySQLDataContext> contextOptions) : base(contextOptions)
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
