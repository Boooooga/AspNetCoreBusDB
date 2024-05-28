using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Bus
{
    public int Id { get; set; }

    public string Model { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<Checkup> Checkups { get; set; } = new List<Checkup>();

    public virtual ICollection<RouteList> RouteLists { get; set; } = new List<RouteList>();
}
