using Microsoft.EntityFrameworkCore;
using TienDatQLTV.Models;

namespace TienDatQLTV.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasKey(b => b.BookID);
            modelBuilder.Entity<Author>().HasKey(a => a.AuthorID);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
            modelBuilder.Entity<Member>().HasKey(m => m.MemberID);
            modelBuilder.Entity<Loan>().HasKey(l => l.LoanID);
            modelBuilder.Entity<Admin>().HasKey(ad => ad.AdminID);

            // Cascade delete for loans when a book is deleted
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookID)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<User> RegisterUser(string username, string password, int roleId)
        {
            var user = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                RoleID = roleId
            };

            Users.Add(user);
            await SaveChangesAsync();

            return user;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}
