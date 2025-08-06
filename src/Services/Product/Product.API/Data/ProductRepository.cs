using System;
using BuildingBlocks.Data;
using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data;

public class ProductRepository : RepositoryBase<Product>
{
    public ProductRepository(ProductContext context) : base(context)
    {
    }
}
