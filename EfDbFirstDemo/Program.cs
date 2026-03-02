using DbModels;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
const string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ApiDev;Integrated Security=True";
optionsBuilder.UseSqlServer(connStr);

using var context = new SchoolContext(optionsBuilder.Options);

var coursesWithAssignments = context.Courses
    .Include(c => c.Assignments)
    .OrderBy(c => c.Title)
    .Select(c => new
    {
        CourseId = c.Id,
        CourseTitle = c.Title,
        Assignments = c.Assignments
            .OrderBy(a => a.Title)
            .Select(a => new { a.Id, a.Title })
            .ToList()
    })
    .ToList();

foreach (var course in coursesWithAssignments)
{
    Console.WriteLine($"[{course.CourseId}] {course.CourseTitle}");
    if (course.Assignments.Count == 0)
    {
        Console.WriteLine("  (no assignments)");
    }
    else
    {
        foreach (var a in course.Assignments)
        {
            Console.WriteLine($"  [{a.Id}] {a.Title}");
        }
    }
}