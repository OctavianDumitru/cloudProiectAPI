using System;
using System.Collections.Generic;

namespace myGarage.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = null!;
}
