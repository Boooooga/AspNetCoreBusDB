using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class View2
{
    public int IdRoute { get; set; }

    public TimeSpan StartTime { get; set; }

    public TimeSpan FinishTime { get; set; }
}
