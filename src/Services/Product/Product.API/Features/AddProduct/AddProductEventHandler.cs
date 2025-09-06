using BuildingBlocks.CQRS;
using MediatR;
using Products.Data;
using Products.Models;

namespace Products.Features.AddProduct;

public record AddProductCommand(Products.Models.Product Product): ICommand<AddProductResult>;

public record AddProductResult(Products.Models.Product Product);

public class AddProductEventHandler
    (ProductRepository _repository)
    : ICommandHandler<AddProductCommand, AddProductResult>
{
    public async Task<AddProductResult> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var result = await _repository.Create(command.Product);

        return new AddProductResult(result);
    }
}
