using System;
using System.Collections.Generic;

namespace Legos.Models;

public partial class Order
{
    public int TransactionId { get; set; }

    public int? CustomerId { get; set; }

    public string? Date { get; set; }

    public string? Dayofweek { get; set; }

    public int? Time { get; set; }

    public string? Entrymode { get; set; }

    public int? Amount { get; set; }

    public string? Typeoftransaction { get; set; }

    public string? Countryoftransaction { get; set; }

    public string? Shippingaddress { get; set; }

    public string? Bank { get; set; }

    public string? Typeofcard { get; set; }

    public int? Fraud { get; set; }
}
