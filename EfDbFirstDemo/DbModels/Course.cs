using System;
using System.Collections.Generic;

namespace DbModels;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
