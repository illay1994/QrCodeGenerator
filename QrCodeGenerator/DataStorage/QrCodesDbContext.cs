using System;
using System.Data.Entity;
using SQLite.CodeFirst;

namespace QrCodeGenerator.DataStorage
{
    internal class QrCodesDbContext : DbContext
    {
        public DbSet<QrCode> QrCodes { get; set; }

        public QrCodesDbContext()
            : base("QrCodesDb")
        {
            Database.Log = Console.Write;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<QrCodesDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

    }
}
