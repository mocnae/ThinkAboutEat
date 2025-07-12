using System;

namespace Products.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Kalor { get; set; }
    public decimal Belk { get; set; }
    public decimal Jir { get; set; }
    public decimal Uglev { get; set; }
}
