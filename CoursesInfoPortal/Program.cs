using CoursesInfoPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCoursesFE",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Register the csv data service with dependency injection
var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
var dataDirectory = Path.Combine(projectDirectory, "Data");

var csvStudentInfoFilePath = Path.Combine(dataDirectory, "students_contacted_information.csv");
var csvCoursesFilePath = Path.Combine(dataDirectory, "technical_assignment_input_data.csv");

builder.Services.AddSingleton<StudentInfoService>(new StudentInfoService(csvStudentInfoFilePath));
builder.Services.AddSingleton<CourseService>(new CourseService(csvCoursesFilePath));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowCoursesFE");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
