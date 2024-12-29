using dormitory_management_be.Models;
using Microsoft.EntityFrameworkCore;

namespace dormitory_management_be.Data
{
    public class DormitoryContext: DbContext
    {
        public DormitoryContext(DbContextOptions<DormitoryContext> options) : base(options)
        {
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Floor> Floor { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<RoomRegistration> RoomRegistration { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<DateConfig> DateConfig { get; set; }
        public DbSet<AdminAccount> AdminAccount { get; set; }
    }
}
