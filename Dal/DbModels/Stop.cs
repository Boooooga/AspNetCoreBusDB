using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Stop
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Adress { get; set; }

    public virtual ICollection<BusRoutesStop> BusRoutesStops { get; set; } = new List<BusRoutesStop>();
}
