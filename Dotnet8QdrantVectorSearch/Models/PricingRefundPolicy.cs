using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class PricingRefundPolicy
{
    public int Id { get; set; }

    public string PlanName { get; set; }

    public int CantRefundDay { get; set; }

    public virtual ICollection<Pricing> Pricings { get; set; } = new List<Pricing>();
}
