using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Building
{
    public int BuildingId { get; set; }

    public int OwnerId { get; set; }

    public string BuildingName { get; set; }

    public string BuildingDescription { get; set; }

    public string BuildingAddress { get; set; }

    public string BuildingDistrict { get; set; }

    public string BuildingCountry { get; set; }

    public string BuildingCity { get; set; }

    public string BuildingZip { get; set; }

    public int? BuildingPayment { get; set; }

    public int? BuildingPromotion { get; set; }

    public byte[] BuildingCreated { get; set; }

    public double BuildingLatitude { get; set; }

    public double BuildingLongitude { get; set; }

    public string BuildingAdvertise { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<BuildingFacility> BuildingFacilities { get; set; } = new List<BuildingFacility>();

    public virtual Owner Owner { get; set; }

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
