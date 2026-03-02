using DbModels;
using Microsoft.EntityFrameworkCore;

var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
const string connStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ApiDev;Integrated Security=True";
optionsBuilder.UseSqlServer(connStr);

using var context = new SchoolContext(optionsBuilder.Options);


var course1 = new Course { Id = 5, Title = "kjhkjh" };
context.Courses.Add(course1);
context.SaveChanges();



return;
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

/*
SELECT c.Id        AS CourseId,
          c.Title     AS CourseTitle,
          a.Id        AS AssignmentId,
          a.Title     AS AssignmentTitle
   FROM Course c
   LEFT JOIN Assignment a ON a.CourseId = c.Id
   ORDER BY c.Title, a.Title;

var coursesWithAssignments =
    (from c in context.Courses
        orderby c.Title
        select new
        {
            CourseId = c.Id,
            CourseTitle = c.Title,
            Assignments =
                (from a in c.Assignments
                    orderby a.Title
                    select new
                    {
                        a.Id,
                        a.Title
                    }).ToList()
        })
    .Include(c => c.Assignments) // fortsatt nødvendig for EF Core eager loading
    .ToList();
*/

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