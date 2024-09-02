using CoursesInfoPortal.Models.Entities;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;
using CoursesInfoPortal.Models;

namespace CoursesInfoPortal.Services
{
    public class StudentInfoService
    {
        private readonly string _csvFilePath;

        public StudentInfoService(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {

            try
            {

                using var reader = new StreamReader(_csvFilePath, Encoding.UTF8);
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";",
                    HasHeaderRecord = true,
                };
                using var csv = new CsvReader(reader, config);

                return await csv.GetRecordsAsync<Student>().ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while accessing students info.", ex);
            }
        }
        public async Task SaveStudentInfoAsync(AddStudentInfoDto addStudentInfoDto)
        {

            var student = new Student
            {
                StudentId = Guid.NewGuid().ToString().ToUpper(),
                FullName = addStudentInfoDto.FullName,
                EmailAddress = addStudentInfoDto.EmailAddress,
                PhoneNumber = addStudentInfoDto.PhoneNumber,
                SelectedCourses = addStudentInfoDto.SelectedCourses,
                Message = addStudentInfoDto.Message,
                ContactedDate = addStudentInfoDto.ContactedDate,
            };
            await Task.Run(() =>
            {
                using var writer = new StreamWriter(_csvFilePath, append: true, encoding: Encoding.UTF8);
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";"
                };
                using var csv = new CsvWriter(writer, config);
                {
                    csv.WriteRecord(student);
                    csv.NextRecord();
                }
            });
        }

    }
}
