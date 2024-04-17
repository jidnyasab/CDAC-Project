using System;
using System.Collections.Generic;

namespace PurposeBuddy.Models.Entities;

public partial class Stripecustomer
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string StripeCustomerId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
