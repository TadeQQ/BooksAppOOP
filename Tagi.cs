using System;
using System.Collections.Generic;

namespace CertificationsApp;

public partial class Tagi
{
    public int TagId { get; set; }

    public string? Nazwa { get; set; }

    public virtual ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
