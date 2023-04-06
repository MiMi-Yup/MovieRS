using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class Movie
{
    public int Id { get; set; }

    public int? IdTmdb { get; set; }

    public int? IdImdb { get; set; }

    public short? YearRelease { get; set; }

    public virtual ICollection<Favourite> Favourites { get; } = new List<Favourite>();

    public virtual ICollection<History> Histories { get; } = new List<History>();

    public virtual ICollection<RawTrainingModel> RawTrainingModels { get; } = new List<RawTrainingModel>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
