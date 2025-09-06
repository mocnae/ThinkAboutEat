using BuildingBlocks.CQRS;
using Product.API.Dtos;
using Products.Data;

namespace Product.API.Features.AddProduct.GetKBJUForRecipe;

public record GetKBJUQuery(List<IngriridientDto> dto) : IQuery<GetKBJUResult>;

public record GetKBJUResult(KBJUdto model);

public class GetKBJUCommandEventHandler
    (ProductRepository _repository)
    : IQueryHandler<GetKBJUQuery, GetKBJUResult>
{
    public async Task<GetKBJUResult> Handle(GetKBJUQuery command, CancellationToken cancellationToken)
    {
        var result = _repository.GetKBJU(command.dto);

        return new GetKBJUResult(result);
    }
}
