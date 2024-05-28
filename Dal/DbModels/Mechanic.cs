using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class Mechanic
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }

    public virtual ICollection<Checkup> Checkups { get; set; } = new List<Checkup>();
}
