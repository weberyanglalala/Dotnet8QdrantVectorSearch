using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class DailyRoomPrice
{
    public int DailyRoomPricingId { get; set; }

    public int RoomId { get; set; }

    public DateOnly Date { get; set; }

    public int PricingId { get; set; }

    public decimal? FinalPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Pricing Pricing { get; set; }

    public virtual Room Room { get; set; }
}
