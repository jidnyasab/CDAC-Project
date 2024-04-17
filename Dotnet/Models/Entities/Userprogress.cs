using System;
using System.Collections.Generic;

namespace PurposeBuddy.Models.Entities;

public partial class Userprogress
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ChapterId { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Chapter Chapter { get; set; } = null!;
}
