using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class RouteList
{
    public int Id { get; set; }

    public DateTime RouteDate { get; set; }

    public int IdBus { get; set; }

    public int IdDriver { get; set; }

    public int? IdConductor { get; set; }

    public int IdRoute { get; set; }

    public virtual Bus IdBusNavigation { get; set; }

    public virtual Conductor IdConductorNavigation { get; set; }

    public virtual Driver IdDriverNavigation { get; set; }

    public virtual BusRoute IdRouteNavigation { get; set; }
}
