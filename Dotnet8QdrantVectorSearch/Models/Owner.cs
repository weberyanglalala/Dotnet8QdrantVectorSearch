using System;
using System.Collections.Generic;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class Owner
{
    public int OwnerId { get; set; }

    public int UserId { get; set; }

    public string OwnerFirstName { get; set; }

    public string OwnerLastName { get; set; }

    public DateOnly OwnerBirthday { get; set; }

    public string OwnerCountry { get; set; }

    public string OwnerLivingCity { get; set; }

    public string OwnerLivingArea { get; set; }

    public string OwnerLivingAddress { get; set; }

    public string OwnerLivingZip { get; set; }

    public string OwnerLanguage { get; set; }

    public string OwnerCellPhone { get; set; }

    public string OwnerEmail { get; set; }

    public byte[] OwnerCreated { get; set; }

    public virtual ICollection<Building> Buildings { get; set; } = new List<Building>();
}
