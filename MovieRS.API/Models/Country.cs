using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class Country
{
    public byte Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
