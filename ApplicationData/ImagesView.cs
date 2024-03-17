using System;
using System.Collections.Generic;

namespace Backend_InkSketch.ApplicationData;

public partial class ImagesView
{
    public string SourceImage { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string HexColor { get; set; } = null!;

    public int ImageId { get; set; }

    public int TagId { get; set; }
}
