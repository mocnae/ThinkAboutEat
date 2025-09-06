using System;
using Recipe.Domain.Abstractions;
using Recipe.Domain.Events;
using Recipe.Domain.ValueObjects;

namespace Recipe.Domain.Models;

public class Recipe : Aggregate<RecipeId>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Kalor { get; private set; }
    public decimal Belk { get; private set; }
    public decimal Jir { get; private set; }
    public decimal Uglev { get; private set; }

    private readonly List<RecipeIngridient> _recipeIngridients = new();
    public IReadOnlyList<RecipeIngridient> RecipeIngridients => _recipeIngridients.AsReadOnly();

    
    private readonly List<RecipeStep> _recipeSteps = new();
    public IReadOnlyList<RecipeStep> RecipeSteps => _recipeSteps;

    public static Recipe Create(string name, string description, decimal kalor, decimal belk, decimal jir, decimal uglev)
    {
        var recipe = new Recipe
        {
            Id = RecipeId.Of(Guid.NewGuid()),
            Name = name,
            Description = description,
            Kalor = kalor,
            Belk = belk,
            Jir = jir,
            Uglev = uglev
        };

        return recipe;
    }

    public void Update(string name, string description, decimal kalor, decimal belk, decimal jir, decimal uglev)
    {
        Name = name;
        Description = description;
        Kalor = kalor;
        Belk = belk;
        Jir = jir;
        Uglev = uglev;
    }

    public void AddStep(RecipeId recipeId, string name, short stepNumber, string description, string photoPath)
    {
        _recipeSteps.Add(new RecipeStep
        {
            Id = RecipeStepId.Of(Guid.NewGuid()),
            RecipeId = recipeId,
            Name = name,
            StepNumber = stepNumber,
            Description = description,
            PhotoPath = photoPath
        });
    }

    public void AddIngridient(Guid id, RecipeId recipeId, string name, int gramm)
    {
        _recipeIngridients.Add(new RecipeIngridient
        {
            Id = RecipeIngridientId.Of(id),
            RecipeId = recipeId,
            Name = name,
            Gramm = gramm
        });

        if (!DomainEvents.Any(x => x.GetType() == typeof(IngridientsChangedEvent)))
        {
            AddDomainEvent(new IngridientsChangedEvent(this));
        }
    }

    public void RemoveStep(RecipeStepId stepId)
    {
        var step = _recipeSteps.FirstOrDefault(x => x.Id == stepId);

        if (step is not null)
        {
            _recipeSteps.Remove(step);
        }
    }

    public void RemoveIngrigient(RecipeIngridientId ingridientId)
    {
        var ingridient = _recipeIngridients.FirstOrDefault(x => x.Id == ingridientId);

        if (ingridient is not null)
        {
            _recipeIngridients.Remove(ingridient);
        }

        AddDomainEvent(new IngridientsChangedEvent(this));
    }
}
