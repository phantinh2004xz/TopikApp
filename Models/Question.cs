using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopikApp.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } // Nội dung câu hỏi

        [Required]
        public string Option1 { get; set; }

        [Required]
        public string Option2 { get; set; }

        public string? Option3 { get; set; } // Optional (câu hỏi TOPIK I có thể chỉ có 2 lựa chọn)
        public string? Option4 { get; set; }

        [Required]
        public int CorrectAnswer { get; set; } // 1-4 tương ứng với Option1-Option4

        // Khóa ngoại đến Exam
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}