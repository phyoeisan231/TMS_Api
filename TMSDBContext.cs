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
        public DbSet<TrailerType> TrailerType { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Transporter> Transporter { get; set; }
        public DbSet<Gate> Gate { get; set; }
    }
}
