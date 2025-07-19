using System;
using BuildingBlocks.CQRS;
using Mapster;
using Products.Data;
using Products.Models;

namespace Products.Features.UpdateProduct;

public record UpdateProductCommand(Product Product): ICommand<UpdateProductCommandResult>;

public record UpdateProductCommandResult(Product Product);

public class UpdateProductEventHandler
    (ProductRepository repository)
    : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
{
    public async Task<UpdateProductCommandResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var entity = await repository.FindById(command.Product.Id);

        if (entity is null)
            throw new ArgumentException("invalid product id");

        entity.Name = command.Product.Name;
        entity.Belk = command.Product.Belk;
        entity.Kalor = command.Product.Kalor;
        entity.Jir = command.Product.Jir;
        entity.Uglev = command.Product.Uglev;

        await repository.Update(entity);

        return new UpdateProductCommandResult(entity);
    }
}
