using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TienDatQLTV.Models;

namespace TienDatQLTV.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Kiểm tra nếu đã có người dùng
                if (context.Users.Any())
                {
                    return;   // Đã có người dùng
                }

                // Thêm role mới
                var adminRole = new Role { RoleName = "admin" };
                var userRole = new Role { RoleName = "user" };

                context.Roles.AddRange(adminRole, userRole);
                context.SaveChanges();

                // Thêm người dùng mới với mật khẩu được băm
                var user1 = new User
                {
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin"),  // Mật khẩu băm
                    Role = adminRole
                };

                var user2 = new User
                {
                    Username = "nguoidung",
                    Password = BCrypt.Net.BCrypt.HashPassword("nguoidung"),  // Mật khẩu băm
                    Role = userRole
                };

                context.Users.AddRange(user1, user2);
                context.SaveChanges();
            }
        }
    }
}
