using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class NonDefaultPricingDate
{
    public int NonDefaultPricingDateId { get; set; }

    public int RoomId { get; set; }

    public int PricingId { get; set; }

    public DateOnly ApplicationDate { get; set; }

    public byte[] NonDefaultPricingDateCreated { get; set; }

    public virtual Pricing Pricing { get; set; }

    public virtual Room Room { get; set; }
}
