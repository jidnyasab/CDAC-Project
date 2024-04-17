﻿using System;
using System.Collections.Generic;

namespace PurposeBuddy.Models.Entities;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
