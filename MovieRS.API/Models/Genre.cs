using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class Genre
{
    public byte Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();
}
