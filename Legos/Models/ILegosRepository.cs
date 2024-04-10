﻿using Microsoft.Build.Evaluation;

namespace Legos.Models
{
    public interface ILegosRepository
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Customer> Customers { get; }
    }
}
