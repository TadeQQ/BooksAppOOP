using System;
using System.Collections.Generic;

namespace CertificationsApp;

public partial class TaskTag
{
    public int TaskTagId { get; set; }

    public int? TaskId { get; set; }

    public int? TagId { get; set; }

    public virtual Tagi? Tag { get; set; }

    public virtual Task? Task { get; set; }
}
