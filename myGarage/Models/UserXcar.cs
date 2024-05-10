using System;
using System.Collections.Generic;

namespace myGarage.Models;

public partial class UserXcar
{
    public int RecordId { get; set; }

    public int UserId { get; set; }

    public int BrandId { get; set; }

    public int ModelId { get; set; }
}
