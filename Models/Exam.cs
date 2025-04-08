using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopikApp.Models
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } // Ví dụ: "TOPIK I - Đề số 1"

        [Required]
        public int Level { get; set; } // Cấp độ TOPIK (1-6)

        [Required]
        public int TimeLimit { get; set; } // Thời gian làm bài (phút)

        // Quan hệ 1-N với Question
        public ICollection<Question> Questions { get; set; }

        // Quan hệ 1-N với ExamResult
        public ICollection<ExamResult> ExamResults { get; set; }
    }
}