using System;
using System.Collections.Generic;

namespace Legos.Models;

public partial class AspNetUserClaim
{
    public string? Id { get; set; }

    public string? UserId { get; set; }

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }
}
