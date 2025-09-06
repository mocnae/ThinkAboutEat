using System;
using System.ComponentModel.DataAnnotations;
using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using MediatR.Entities;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse> 
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var failures = validationResults.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

        if (failures.Any())
            throw new FluentValidation.ValidationException(string.Join(", ", failures));

        return await next();
    }
}
