using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Conductor
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public virtual ICollection<RouteList> RouteLists { get; set; } = new List<RouteList>();
}
