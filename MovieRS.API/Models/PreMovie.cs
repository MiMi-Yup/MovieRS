using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class PreMovie
{
    public long MovieId { get; set; }

    public long TmdbId { get; set; }

    public virtual ICollection<PreTraining> PreTrainings { get; set; } = new List<PreTraining>();
}
