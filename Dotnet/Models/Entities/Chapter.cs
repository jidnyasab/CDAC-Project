using System;
using System.Collections.Generic;

namespace PurposeBuddy.Models.Entities;

public partial class Chapter
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? VideoUrl { get; set; }

    public int Position { get; set; }

    public bool IsPublished { get; set; }

    public bool IsFree { get; set; }

    public string CourseId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Muxdatum? Muxdatum { get; set; }

    public virtual ICollection<Userprogress> Userprogresses { get; set; } = new List<Userprogress>();
}
