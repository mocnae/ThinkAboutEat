using System;
using BuildingBlocks.CQRS;
using Products.Data;
using Products.Models;

namespace Products.Features.GetProduct;

public record GetProductQuery(string Name): IQuery<GetProductResult>;
public record GetProductResult(Products.Models.Product Product);

public class GetProductEventHandler
    (ProductRepository repository)
    : IQueryHandler<GetProductQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.FindByNameAsync(query.Name);

        return new GetProductResult(result);
    }
}
