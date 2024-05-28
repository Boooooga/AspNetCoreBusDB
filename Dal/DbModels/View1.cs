using System;
using System.Collections.Generic;

namespace Dal.DbModels;

public partial class View1
{
    public string Автобус { get; set; }

    public string Водитель { get; set; }

    public int? ВозрастВодителя { get; set; }

    public string Кондуктор { get; set; }

    public DateTime? Дата { get; set; }

    public int? Маршрут { get; set; }
}
