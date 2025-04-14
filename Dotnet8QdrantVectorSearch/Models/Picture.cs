using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Picture
{
    public int PictureId { get; set; }

    public int? BuildingId { get; set; }

    public string PictureUrl { get; set; }

    public string PictureCaption { get; set; }

    public byte[] PictureCreated { get; set; }

    public virtual Building Building { get; set; }

    public virtual ICollection<PicturePictureTag> PicturePictureTags { get; set; } = new List<PicturePictureTag>();
}
