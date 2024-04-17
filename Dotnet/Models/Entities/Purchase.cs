using System;
using System.Collections.Generic;

namespace PurposeBuddy.Models.Entities;

public partial class Purchase
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string CourseId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;
}
