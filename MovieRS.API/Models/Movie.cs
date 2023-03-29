using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class Movie
{
    public int Id { get; set; }

    public int? IdTmdb { get; set; }

    public int? IdImdb { get; set; }

    public DateTime? YearRelease { get; set; }

    public virtual ICollection<History> Histories { get; } = new List<History>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
