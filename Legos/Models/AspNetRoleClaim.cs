using System;
using System.Collections.Generic;

namespace Legos.Models;

public partial class AspNetRoleClaim
{
    public string? Id { get; set; }

    public string? RoleId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
