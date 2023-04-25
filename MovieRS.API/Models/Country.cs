using System;
using System.Collections.Generic;

namespace MovieRS.API.Models;

public partial class Country
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
