using System;
using System.Net.Http.Json;
using BuildingBlocks.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Recipe.Application.Dtos;
using Recipe.Domain.Events;

namespace Recipe.Application.Recipes.EventHandlers;

public class IngridientsChangedEventHandler : INotificationHandler<IngridientsChangedEvent>
{
    public async Task Handle(IngridientsChangedEvent notification, CancellationToken cancellationToken)
    {
        using (var httpClient = new HttpClient())
        {
            var data = new
            {
                dto = notification.recipe.RecipeIngridients.Select(x => new
                {
                    Id = x.Id.Value,
                    x.Gramm
                }).ToList()
            };

            HttpResponseMessage response = default;
            try
            {
                response = await httpClient.PostAsJsonAsync("http://localhost:6000/products/getkbju", data);
            }
            catch (Exception)
            {
                throw new NotFoundException("cant count kbju");
            }

            if (!response.IsSuccessStatusCode)
                throw new BadRequestException("error in counting kbju");

            var returnedData = await response.Content.ReadFromJsonAsync<KBJUDto>();

            if (returnedData is null)
                throw new BadRequestException("invalid response from products service");

            notification.recipe.Update(
                notification.recipe.Name, notification.recipe.Description, returnedData.Kalor, returnedData.Belk, returnedData.Jir, returnedData.Uglev
            );
        }
    }
}
