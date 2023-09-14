using System;
using System.Collections.Generic;

namespace FirstWebApp.EntitiesFromDb;

public partial class Product
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public int? Amount { get; set; }

    public string? Producer { get; set; }
}
