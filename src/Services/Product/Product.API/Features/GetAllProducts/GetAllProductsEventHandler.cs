using System;
using BuildingBlocks.CQRS;
using Products.Data;
using Products.Models;

namespace Products.Features.GetAllProducts;

public record GetAllProductsQuery() : IQuery<GetAllProductsResult>;

public record GetAllProductsResult(List<Products.Models.Product> Products);

public class GetAllProductsEventHandler
    (ProductRepository repository)
    : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
{
    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await repository.GetAll();

        return new GetAllProductsResult(products);
    }
}
