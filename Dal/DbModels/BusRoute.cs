using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class BusRoute
{
    public int Id { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan FinishTime { get; set; }

    public virtual ICollection<BusRoutesStop> BusRoutesStops { get; set; } = new List<BusRoutesStop>();

    public virtual ICollection<RouteList> RouteLists { get; set; } = new List<RouteList>();
}
