using System;
using System.Collections.Generic;

namespace Backend_InkSketch.ApplicationData;

public partial class ProfileView
{
    public string Tagname { get; set; } = null!;

    public string? Username { get; set; }

    public int UserId { get; set; }

    public int TagId { get; set; }
}
