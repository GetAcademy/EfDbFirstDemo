using System;
using System.Collections.Generic;

namespace DbModels;

public partial class Student
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
