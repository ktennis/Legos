using System;
using System.Collections.Generic;

namespace Legos.Models;

public partial class Customer
{
    public int? CustomerId { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Birthdate { get; set; }

    public string? Countryofresidence { get; set; }

    public string? Gender { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }
}
