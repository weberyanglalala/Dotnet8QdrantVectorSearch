using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BookingId { get; set; }

    public DateTime? ReviewDate { get; set; }

    public double? OverallRating { get; set; }

    public bool? Recommendation { get; set; }

    public int? CostPerformanceRatio { get; set; }

    public int? PositionRating { get; set; }

    public int? CleanRating { get; set; }

    public int? ServiceRating { get; set; }

    public int? FacilityRating { get; set; }

    public string CustomerType { get; set; }

    public string ReviewTitle { get; set; }

    public string ReviewComment { get; set; }

    public string OwnerReponse { get; set; }

    public DateTime? OwnerReponseDate { get; set; }

    public virtual Booking Booking { get; set; }
}
