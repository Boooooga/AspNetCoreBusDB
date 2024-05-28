using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Driver
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public int Experience { get; set; }

    public virtual ICollection<RouteList> RouteLists { get; set; } = new List<RouteList>();
}
