using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class PicturePictureTag
{
    public int PicturePictureTagId { get; set; }

    public int PictureId { get; set; }

    public int PictureTagId { get; set; }

    public virtual Picture Picture { get; set; }

    public virtual PictureTag PictureTag { get; set; }
}
