using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using TopikApp.Models;

namespace TopikApp.Helpers
{
    public static class ExcelExamHelper  // Đổi tên class
    {
        public static Exam ImportExamFromExcel(string filePath)  // Đổi tên method
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(filePath));
            var worksheet = package.Workbook.Worksheets[0];

            var exam = new Exam
            {
                Title = worksheet.Cells["A1"].GetValue<string>() ?? "Untitled Exam",
                Level = worksheet.Cells["B1"].GetValue<int>(),
                TimeLimit = worksheet.Cells["C1"].GetValue<int>(),
                Questions = new List<Question>()
            };

            for (int row = 2; worksheet.Cells[row, 1].Value != null; row++)
            {
                exam.Questions.Add(new Question
                {
                    Content = worksheet.Cells[row, 1].GetValue<string>() ?? string.Empty,
                    Option1 = worksheet.Cells[row, 2].GetValue<string>() ?? string.Empty,
                    Option2 = worksheet.Cells[row, 3].GetValue<string>() ?? string.Empty,
                    Option3 = worksheet.Cells[row, 4]?.GetValue<string>(),
                    Option4 = worksheet.Cells[row, 5]?.GetValue<string>(),
                    CorrectAnswer = worksheet.Cells[row, 6].GetValue<int>()
                });
            }

            return exam;
        }
    }
}