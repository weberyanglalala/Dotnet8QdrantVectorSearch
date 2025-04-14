using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class PictureTag
{
    public int PictureTagId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<PicturePictureTag> PicturePictureTags { get; set; } = new List<PicturePictureTag>();
}
