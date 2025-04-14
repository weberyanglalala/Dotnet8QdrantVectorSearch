using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int BuildingId { get; set; }

    public string RoomName { get; set; }

    public int RoomNum { get; set; }

    public int RoomBedroomNum { get; set; }

    public string RoomBedroomBedSet { get; set; }

    public int RoomBedroomBedNum { get; set; }

    public int RoomLivingMax { get; set; }

    public string RoomDescription { get; set; }

    public decimal RoomPrice { get; set; }

    public int RoomRestroomNum { get; set; }

    public byte[] RoomCreated { get; set; }

    public bool RoomHasBreakfast { get; set; }

    public decimal? RoomBreakfastPrice { get; set; }

    public bool RoomHasLunch { get; set; }

    public decimal? RoomLunchPrice { get; set; }

    public bool RoomHasDinner { get; set; }

    public decimal? RoomDinnerPrice { get; set; }

    public double RoomSize { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Building Building { get; set; }

    public virtual ICollection<DailyRoomPrice> DailyRoomPrices { get; set; } = new List<DailyRoomPrice>();

    public virtual ICollection<NonDefaultPricingDate> NonDefaultPricingDates { get; set; } = new List<NonDefaultPricingDate>();
}
