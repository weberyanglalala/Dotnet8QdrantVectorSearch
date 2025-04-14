using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public string FacilityName { get; set; }

    public string FacilityIcon { get; set; }

    public byte[] FacilityCreated { get; set; }

    public virtual ICollection<BuildingFacility> BuildingFacilities { get; set; } = new List<BuildingFacility>();
}
