using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Pricing
{
    public int PricingId { get; set; }

    public string PricingName { get; set; }

    public int PricingRefundPolicy { get; set; }

    public decimal PriceCoefficient { get; set; }

    public byte[] PricingCreated { get; set; }

    public virtual ICollection<DailyRoomPrice> DailyRoomPrices { get; set; } = new List<DailyRoomPrice>();

    public virtual ICollection<NonDefaultPricingDate> NonDefaultPricingDates { get; set; } = new List<NonDefaultPricingDate>();

    public virtual PricingRefundPolicy PricingRefundPolicyNavigation { get; set; }
}
