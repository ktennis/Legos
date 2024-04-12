using System;
using System.Collections.Generic;

namespace Legos.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public int? Year { get; set; }

    public int? Numparts { get; set; }

    public int? Price { get; set; }

    public string? Imglink { get; set; }

    public string? Primarycolor { get; set; }

    public string? Secondarycolor { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public int? Rec1 { get; set; }

    public int? Rec2 { get; set; }

    public int? Rec3 { get; set; }

    public int? Rec4 { get; set; }

    public int? Rec5 { get; set; }
}
