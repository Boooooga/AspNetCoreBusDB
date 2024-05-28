using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class BusRoutesStop
{
    public int Id { get; set; }

    public int IdStop { get; set; }

    public int IdRoute { get; set; }

    public virtual ICollection<ArrivalTime> ArrivalTimes { get; set; } = new List<ArrivalTime>();

    public virtual BusRoute IdRouteNavigation { get; set; }

    public virtual Stop IdStopNavigation { get; set; }
}
