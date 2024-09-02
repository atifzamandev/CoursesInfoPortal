using CoursesInfoPortal.Models;
using CoursesInfoPortal.Models.Entities;
using CoursesInfoPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoursesInfoPortal.Controllers
{
    [Route("api/studentinfo")]
    [ApiController]
    public class StudentInfoController : ControllerBase
    {
        private readonly StudentInfoService _studentInfoService;

        public StudentInfoController(StudentInfoService studentInfoService)
        {
            _studentInfoService = studentInfoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            var studentInfo = await _studentInfoService.GetStudentsAsync();
            return Ok(studentInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddStudentInfoDto addStudentInfoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _studentInfoService.SaveStudentInfoAsync(addStudentInfoDto);
                return Ok(new { message = "Student information saved successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }  
            
        }


    }
}
