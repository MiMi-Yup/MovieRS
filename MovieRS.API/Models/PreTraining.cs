using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class PreTraining
{
    public long UserId { get; set; }

    public long MovieId { get; set; }

    public double? Rating { get; set; }

    public virtual PreMovie Movie { get; set; } = null!;
}
