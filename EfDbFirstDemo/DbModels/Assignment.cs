using System;
using System.Collections.Generic;

namespace DbModels;

public partial class Assignment
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
