using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class RawTrainingModel
{
    public int UserId { get; set; }

    public int MovieId { get; set; }

    public decimal? Rating { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
