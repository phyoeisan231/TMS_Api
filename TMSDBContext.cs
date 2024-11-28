using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TMS_Api.Configuration;
using TMS_Api.DBModels;
namespace TMS_Api
{
    public class TMSDBContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;
        public TMSDBContext(DbContextOptions<TMSDBContext> options, IConfiguration configuration)
           : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<InBoundCheckDocument>().HasKey(m => new { m.InRegNo, m.DocCode });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<TruckType> TruckType { get; set; }
        public DbSet<Truck> Truck { get; set; }
        public DbSet<Trailer> Trailer { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<TransporterType> TransporterType { get; set; }
        public DbSet<Transporter> Transporter { get; set; }
        public DbSet<Gate> Gate { get; set; }
        public DbSet<Yard> Yard { get; set; }
        public DbSet<TruckJobType> TruckJobType { get; set; }
        public DbSet<TruckEntryType> TruckEntryType { get; set; }
        public DbSet<WeightBridge> WeightBridge { get; set; }
        public DbSet<WaitingArea> WaitingArea { get; set; }
        public DbSet<PCategory> PCategory { get; set; }
        public DbSet<PCard> PCard { get; set; }
        public DbSet<InBoundCheck> InBoundCheck { get; set; }
        public DbSet<InBoundCheckDocument> InBoundCheckDocument { get; set; }
        public DbSet<TruckProcess> TruckProcess { get; set; }
        public DbSet<DocumentSetting> DocumentSetting { get; set; }    
    }
}
