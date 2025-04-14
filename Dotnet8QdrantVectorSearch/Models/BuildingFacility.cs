using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class BuildingFacility
{
    public int BuildingFacilityId { get; set; }

    public int BuildingId { get; set; }

    public int FacilityId { get; set; }

    public byte[] BuildingFacilityCreated { get; set; }

    public virtual Building Building { get; set; }

    public virtual Facility Facility { get; set; }
}
