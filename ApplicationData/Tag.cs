using System;
using System.Collections.Generic;

namespace Backend_InkSketch.ApplicationData;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public string HexColor { get; set; } = null!;

    public virtual ICollection<ImagesTag> ImagesTags { get; } = new List<ImagesTag>();

    public virtual ICollection<UsersTag> UsersTags { get; } = new List<UsersTag>();
}
