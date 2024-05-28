using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Checkup
{
    public int Id { get; set; }

    public DateTime CheckupDate { get; set; }

    public bool IsOk { get; set; }

    public int IdBus { get; set; }

    public int IdMechanic { get; set; }

    public virtual Bus IdBusNavigation { get; set; }

    public virtual Mechanic IdMechanicNavigation { get; set; }
}
