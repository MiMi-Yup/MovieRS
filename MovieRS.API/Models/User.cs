﻿using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string Email { get; set; } = null!;

    public byte[]? Password { get; set; }

    public short? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
