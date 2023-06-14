using System;
using System.Collections.Generic;

namespace CertificationsApp;

public partial class Task
{
    public int TaskId { get; set; }

    public string? Nazwa { get; set; }

    public string? Opis { get; set; }

    public int? Priorytet { get; set; }

    public DateTime? DataDodania { get; set; }

    public DateTime? TerminWykonania { get; set; }

    public bool? Wykonane { get; set; }

    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
