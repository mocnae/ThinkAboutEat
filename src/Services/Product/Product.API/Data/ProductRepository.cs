using System;
using BuildingBlocks.Data;
using Microsoft.EntityFrameworkCore;
using Product.API.Dtos;
using Products.Models;

namespace Products.Data;

public class ProductRepository : RepositoryBase<Products.Models.Product>
{
    public ProductRepository(ProductContext context) : base(context)
    {
    }

    public KBJUdto GetKBJU(List<IngriridientDto> ingriridients)
    {
        var products = _context.Set<Products.Models.Product>().Where(
            x => ingriridients.Select(i => i.Id).Contains(x.Id)
        ).ToList();

        var result = new KBJUdto
        {
            Kalor = products.Sum(x => x.Kalor * ingriridients.First(s => s.Id == x.Id).Gramm / 100),
            Belk = products.Sum(x => x.Belk * ingriridients.First(s => s.Id == x.Id).Gramm / 100),
            Jir = products.Sum(x => x.Jir * ingriridients.First(s => s.Id == x.Id).Gramm / 100),
            Uglev = products.Sum(x => x.Kalor * ingriridients.First(s => s.Id == x.Id).Gramm / 100),
        };

        return result;
    }
}
