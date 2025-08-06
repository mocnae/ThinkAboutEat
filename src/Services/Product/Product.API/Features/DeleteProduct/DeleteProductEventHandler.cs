using System;
using BuildingBlocks.CQRS;
using Products.Data;

namespace Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductEventHandler
    (ProductRepository repository)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await repository.FindById(command.Id);

        if (product is null)
            throw new ArgumentException("Product with given ID doesnt exist");

        await repository.Delete(product);

        return new DeleteProductResult(true);
    }
}
