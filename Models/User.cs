using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopikApp.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; } // Lưu ý: Nên mã hóa (hash) mật khẩu

        [Required]
        [StringLength(50)]
        public string Role { get; set; } // "Admin" hoặc "User"

        // Quan hệ 1-N với ExamResult
        public ICollection<ExamResult> ExamResults { get; set; }
    }
}