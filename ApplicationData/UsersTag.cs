using System;
using System.Collections.Generic;

namespace Backend_InkSketch.ApplicationData;

public partial class UsersTag
{
    public int UserTagId { get; set; }

    public int UserId { get; set; }

    public int TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
