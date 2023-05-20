using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class Movie
{
    public int Id { get; set; }

    public int? IdTmdb { get; set; }

    public int? IdImdb { get; set; }

    public short? YearRelease { get; set; }

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
