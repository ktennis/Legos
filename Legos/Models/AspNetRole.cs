using System;
using System.Collections.Generic;

namespace Legos.Models;

public partial class AspNetRole
{
    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }
}
