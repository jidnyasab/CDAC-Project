using System;
using System.Collections.Generic;

namespace PurposeBuddy.Models.Entities;

public partial class Muxdatum
{
    public string Id { get; set; } = null!;

    public string AssetId { get; set; } = null!;

    public string? PlaybackId { get; set; }

    public string ChapterId { get; set; } = null!;

    public virtual Chapter Chapter { get; set; } = null!;
}
