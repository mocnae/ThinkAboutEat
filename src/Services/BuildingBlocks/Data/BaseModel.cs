using System;

namespace BuildingBlocks.Data;

public class BaseModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
}
