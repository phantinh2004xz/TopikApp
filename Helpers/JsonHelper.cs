using System;
using System.IO;
using Newtonsoft.Json;
using TopikApp.Models;

namespace TopikApp.Helpers
{
    public static class JsonExamHelper  // Đổi tên class để tránh trùng lặp
    {
        public static Exam ImportExamFromJson(string filePath)  // Đổi tên method
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("File not found", filePath);

                string jsonContent = File.ReadAllText(filePath);
                var exam = JsonConvert.DeserializeObject<Exam>(jsonContent, new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error,
                    NullValueHandling = NullValueHandling.Ignore
                });

                if (exam == null || exam.Questions == null || exam.Questions.Count == 0)
                    throw new InvalidDataException("Invalid exam data format");

                return exam;
            }
            catch (JsonException ex)
            {
                throw new FormatException("Invalid JSON format", ex);
            }
        }
    }
}