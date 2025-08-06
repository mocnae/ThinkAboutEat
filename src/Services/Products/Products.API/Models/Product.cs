using System;
using BuildingBlocks.Data;

namespace Products.Models;

public class Product : BaseModel
{
    public decimal Kalor { get; set; }
    public decimal Belk { get; set; }
    public decimal Jir { get; set; }
    public decimal Uglev { get; set; }
}
