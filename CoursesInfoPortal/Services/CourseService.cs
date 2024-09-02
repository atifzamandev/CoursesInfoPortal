using CoursesInfoPortal.Models.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace CoursesInfoPortal.Services
{
    public class CourseService
    {
        private readonly string _csvFilePath;

        public CourseService(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync()
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

                return await csv.GetRecordsAsync<Course>().ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while accessing courses.", ex);
            }

        }
    }
}
