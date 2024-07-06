using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TienDatQLTV.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }

        [MaxLength(60)]  // Đảm bảo độ dài phù hợp cho mật khẩu băm
        public string Password { get; set; }

        public Role Role { get; set; }
      
            public int RoleID { get; set; }
         
        }
    }


   

