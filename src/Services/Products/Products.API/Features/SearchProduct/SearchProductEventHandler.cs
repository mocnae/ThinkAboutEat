using System;
using BuildingBlocks.CQRS;
using Products.Data;
using Products.Models;

namespace Products.Features.SearchProduct;

public record SearchProductQuery(string name) : IQuery<SearchProductResult>;

public record SearchProductResult(List<Product> Products);

public class SearchProductEventHandler
    (ProductRepository repository)
    : IQueryHandler<SearchProductQuery, SearchProductResult>
{
    public async Task<SearchProductResult> Handle(SearchProductQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.Search(query.name);

        return new SearchProductResult(result);
    }
}
