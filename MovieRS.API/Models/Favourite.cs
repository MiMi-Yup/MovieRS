using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class Favourite
{
    public int UserId { get; set; }

    public int MovieId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
