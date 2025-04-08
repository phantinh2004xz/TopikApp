using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopikApp.Models
{
    public class ExamResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public DateTime CompletedAt { get; set; } // Thời gian hoàn thành bài thi

        // Khóa ngoại đến User
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        // Khóa ngoại đến Exam
        [ForeignKey("Exam")]
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}