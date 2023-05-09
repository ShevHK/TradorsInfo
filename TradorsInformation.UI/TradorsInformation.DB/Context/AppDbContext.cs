using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using TradorsInformation.DB.Entities;

namespace TradorsInformation.DB.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<TradorInfo> TradorsInfo { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);
            modelBuilder?.Entity<TradorInfo>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
