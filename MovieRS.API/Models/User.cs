﻿using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string Email { get; set; } = null!;

    public byte[]? Password { get; set; }

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<History> Histories { get; } = new List<History>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();
}
