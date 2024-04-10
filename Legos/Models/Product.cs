using System;
using System.Collections.Generic;

namespace Legos.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public int? Year { get; set; }

    public int? NumParts { get; set; }

    public int? Price { get; set; }

    public string? ImgLink { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public string? Description { get; set; }

    public string? Category { get; set; }

    public string? Rec1 { get; set; }

    public string? Rec2 { get; set; }

    public string? Rec3 { get; set; }

    public string? Rec4 { get; set; }

    public string? Rec5 { get; set; }
}
