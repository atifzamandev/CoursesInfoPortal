using CoursesInfoPortal.Models.Entities;
using CoursesInfoPortal.Services;
using Microsoft.AspNetCore.Mvc;


namespace CoursesInfoPortal.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {

            _courseService = courseService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            try
            {
                var courses = await _courseService.GetCoursesAsync();
                return Ok(courses);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet]
        [Route("searchcourses")]
        public async Task<ActionResult<IEnumerable<Course>>> SearchCoursesInCsv([FromQuery] string q)
        {
            try
            {
                var courses = await _courseService.GetCoursesAsync();
                var searchedCourses = courses.Where(c => c.CourseName.Contains(q, StringComparison.OrdinalIgnoreCase)).ToList();

                if (!searchedCourses.Any())
                {
                    return NotFound("No courese match found");
                }
                return Ok(searchedCourses);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
