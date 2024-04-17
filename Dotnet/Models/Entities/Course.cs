using System;
using System.Collections.Generic;

namespace PurposeBuddy.Models.Entities;

public partial class Course
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public double? Price { get; set; }

    public bool IsPublished { get; set; }

    public string? CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
