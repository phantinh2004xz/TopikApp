using System;
using TopikApp.Helpers;
using TopikApp.Models;
using TopikApp.Data;

namespace TopikApp.Services
{
    public class ExamImportService
    {
        private readonly AppDbContext _context;

        public ExamImportService(AppDbContext context)
        {
            _context = context;
        }

        public void ImportExamFile(string filePath, FileType fileType)
        {
            Exam exam = fileType switch
            {
                FileType.JSON => JsonExamHelper.ImportExamFromJson(filePath),
                FileType.Excel => ExcelExamHelper.ImportExamFromExcel(filePath),
                _ => throw new NotSupportedException($"Unsupported file type: {fileType}")
            };

            _context.Exams.Add(exam);
            _context.SaveChanges();
        }
    }

    public enum FileType
    {
        JSON,
        Excel
    }
}