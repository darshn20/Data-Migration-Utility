using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataMigrationUtility.Domain;

namespace DataMigrationUtility.Data
{
    public class DataMigrationUtilityDbContext : DbContext
    {
        public DbSet<SourceTable> SourceTable { get; set; }
        public DbSet<DestinationTable> DestinationTable { get; set; }
        public DbSet<MigrationTable> MigrationTable { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=DataMigrationUtilityDb");
        }
    }
}
