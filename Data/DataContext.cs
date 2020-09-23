using HelperDesk.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HelperDesk.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options){ }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<TicketDetail> TicketDetails { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TracingStatus> TracingStatus { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Document> Document { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId)
                .HasConstraintName("FK_Users_Companies_CompanyId");

            modelBuilder.Entity<User>()
                .HasOne(u => u.Gender)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.GenderId)
                .HasConstraintName("FK_Users_Genders_GenderId");

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.RoleId)
                .HasConstraintName("FK_Users_Roles_RoleId");

            modelBuilder.Entity<Company>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<Gender>()
                .Property(g => g.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<TicketCategory>()
                .Property(tc => tc.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<TicketDetail>()
                .Property(td => td.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<TicketStatus>()
                .Property(ts => ts.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<TicketType>()
                .Property(tt => tt.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<TracingStatus>()
                .Property(ts => ts.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<Department>()
                .Property(ts => ts.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<Position>()
                .Property(ts => ts.CreatedAt)
                .HasDefaultValueSql("datetime('now')");

            modelBuilder.Entity<Document>()
                .Property(ts => ts.CreatedAt)
                .HasDefaultValueSql("datetime('now')");
        }
    }
}