using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class ArrivalTime
{
    public int Id { get; set; }

    public TimeSpan Arrival { get; set; }

    public int IdBusRouteStop { get; set; }

    public virtual BusRoutesStop IdBusRouteStopNavigation { get; set; }
}
