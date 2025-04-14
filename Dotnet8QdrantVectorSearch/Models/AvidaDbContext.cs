using Microsoft.EntityFrameworkCore;

namespace Dotnet8QdrantVectorSearch.Models;

public partial class AvidaDbContext : DbContext
{
    public AvidaDbContext()
    {
    }

    public AvidaDbContext(DbContextOptions<AvidaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<BuildingFacility> BuildingFacilities { get; set; }

    public virtual DbSet<DailyRoomPrice> DailyRoomPrices { get; set; }

    public virtual DbSet<EcpayTransaction> EcpayTransactions { get; set; }

    public virtual DbSet<FacebookLogin> FacebookLogins { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<GoogleLogin> GoogleLogins { get; set; }

    public virtual DbSet<LineLogin> LineLogins { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<NonDefaultPricingDate> NonDefaultPricingDates { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<PicturePictureTag> PicturePictureTags { get; set; }

    public virtual DbSet<PictureTag> PictureTags { get; set; }

    public virtual DbSet<Pricing> Pricings { get; set; }

    public virtual DbSet<PricingRefundPolicy> PricingRefundPolicies { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Chinese_Taiwan_Stroke_CI_AS");

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Booking");

            entity.HasIndex(e => e.RoomId, "IX_Booking_RoomID");

            entity.HasIndex(e => e.UserId, "IX_Booking_UserID");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
            entity.Property(e => e.Commission).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ContactPhone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ConversationId).HasColumnName("ConversationID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CustomerCountry).HasMaxLength(50);
            entity.Property(e => e.CustomerFirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CustomerLastName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.GroupOrderId)
                .HasMaxLength(200)
                .HasColumnName("GroupOrderID");
            entity.Property(e => e.OrderPayment)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OrderState).HasMaxLength(50);
            entity.Property(e => e.PaymentState).HasMaxLength(50);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Building).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Building");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("FK_Booking_Messages");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Room");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_User");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.ToTable("Building");

            entity.HasIndex(e => e.OwnerId, "IX_Building_OwnerID");

            entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
            entity.Property(e => e.BuildingAddress)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.BuildingCity)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.BuildingCountry)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.BuildingCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.BuildingDescription).IsRequired();
            entity.Property(e => e.BuildingDistrict).HasMaxLength(50);
            entity.Property(e => e.BuildingName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.BuildingZip)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Building_Owner");
        });

        modelBuilder.Entity<BuildingFacility>(entity =>
        {
            entity.ToTable("BuildingFacility");

            entity.HasIndex(e => e.FacilityId, "IX_BuildingFacility_FacilityID");

            entity.Property(e => e.BuildingFacilityId).HasColumnName("BuildingFacilityID");
            entity.Property(e => e.BuildingFacilityCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

            entity.HasOne(d => d.Building).WithMany(p => p.BuildingFacilities)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingFacility_Building");

            entity.HasOne(d => d.Facility).WithMany(p => p.BuildingFacilities)
                .HasForeignKey(d => d.FacilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BuildingFacility_Facility");
        });

        modelBuilder.Entity<DailyRoomPrice>(entity =>
        {
            entity.HasKey(e => e.DailyRoomPricingId).HasName("PK_DailyRoomPricing");

            entity.ToTable("DailyRoomPrice", tb => tb.HasTrigger("trg_DailyRoomPricing_UpdatedAt"));

            entity.HasIndex(e => new { e.RoomId, e.Date }, "IX_DailyRoomPricing_RoomID_Date");

            entity.Property(e => e.DailyRoomPricingId).HasColumnName("DailyRoomPricingID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FinalPrice).HasColumnType("decimal(10, 1)");
            entity.Property(e => e.PricingId).HasColumnName("PricingID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Pricing).WithMany(p => p.DailyRoomPrices)
                .HasForeignKey(d => d.PricingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyRoomPricing_Pricing");

            entity.HasOne(d => d.Room).WithMany(p => p.DailyRoomPrices)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyRoomPricing_Room");
        });

        modelBuilder.Entity<EcpayTransaction>(entity =>
        {
            entity.HasKey(e => e.GroupOrderId);

            entity.ToTable("EcpayTransaction");

            entity.Property(e => e.GroupOrderId)
                .HasMaxLength(200)
                .HasColumnName("GroupOrderID");
            entity.Property(e => e.MerchantTradeNo)
                .HasMaxLength(50)
                .HasComment("特店訂單編號(我們所定義的，必須唯一)");
            entity.Property(e => e.PaymentDate)
                .HasComment("付款時間")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(20)
                .HasComment("付款方式");
            entity.Property(e => e.PaymentTypeChargeFee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RtnCode).HasComment("交易狀態");
            entity.Property(e => e.RtnMsg)
                .HasMaxLength(200)
                .HasComment("交易訊息");
            entity.Property(e => e.TradeAmt).HasComment("交易金額");
            entity.Property(e => e.TradeDate)
                .HasComment("訂單成立時間")
                .HasColumnType("datetime");
            entity.Property(e => e.TradeNo)
                .HasMaxLength(50)
                .HasComment("綠界的交易編號");
        });

        modelBuilder.Entity<FacebookLogin>(entity =>
        {
            entity.ToTable("FacebookLogin");

            entity.HasIndex(e => e.UserId, "IX_FacebookLogin_UserID");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("外部系統提供的唯一識別碼")
                .HasColumnName("ExternalID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.FacebookLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacebookLogin_User");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.ToTable("Facility");

            entity.Property(e => e.FacilityId).HasColumnName("FacilityID");
            entity.Property(e => e.FacilityCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.FacilityIcon)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FacilityName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<GoogleLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserExternalLogin");

            entity.ToTable("GoogleLogin", tb => tb.HasComment(""));

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("外部系統提供的唯一識別碼")
                .HasColumnName("ExternalID");
            entity.Property(e => e.UserId)
                .HasComment("")
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<LineLogin>(entity =>
        {
            entity.ToTable("LineLogin");

            entity.HasIndex(e => e.UserId, "IX_LineLogin_UserID");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ExternalId)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("外部系統提供的唯一識別碼")
                .HasColumnName("ExternalID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.LineLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LineLogin_User");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasIndex(e => e.BookingId, "IX_Messages_BookingID");

            entity.HasIndex(e => e.UserId, "IX_Messages_UserID");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MessageContent).IsRequired();
            entity.Property(e => e.MessageType)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.Messages)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Booking");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_User");
        });

        modelBuilder.Entity<NonDefaultPricingDate>(entity =>
        {
            entity.ToTable("NonDefaultPricingDate");

            entity.HasIndex(e => e.PricingId, "IX_NonDefaultPricingDate_PricingID");

            entity.HasIndex(e => e.RoomId, "IX_NonDefaultPricingDate_RoomID");

            entity.Property(e => e.NonDefaultPricingDateId).HasColumnName("NonDefaultPricingDateID");
            entity.Property(e => e.NonDefaultPricingDateCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.PricingId).HasColumnName("PricingID");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");

            entity.HasOne(d => d.Pricing).WithMany(p => p.NonDefaultPricingDates)
                .HasForeignKey(d => d.PricingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonDefaultPricingDate_Pricing");

            entity.HasOne(d => d.Room).WithMany(p => p.NonDefaultPricingDates)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonDefaultPricingDate_Room");
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.ToTable("Owner");

            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");
            entity.Property(e => e.OwnerCellPhone)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.OwnerCountry)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OwnerCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.OwnerEmail)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OwnerFirstName)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.OwnerLanguage)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.OwnerLastName)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.OwnerLivingAddress)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OwnerLivingArea)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OwnerLivingCity)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.OwnerLivingZip)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.ToTable("Picture");

            entity.HasIndex(e => e.BuildingId, "IX_Picture_BuildingID");

            entity.Property(e => e.PictureId).HasColumnName("PictureID");
            entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
            entity.Property(e => e.PictureCaption)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PictureCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.PictureUrl)
                .IsRequired()
                .HasColumnName("PictureURL");

            entity.HasOne(d => d.Building).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK_Picture_Building");
        });

        modelBuilder.Entity<PicturePictureTag>(entity =>
        {
            entity.ToTable("Picture_PictureTag");

            entity.HasIndex(e => e.PictureId, "IX_Picture_PictureTag_PictureID");

            entity.HasIndex(e => e.PictureTagId, "IX_Picture_PictureTag_PictureTagID");

            entity.Property(e => e.PicturePictureTagId).HasColumnName("Picture_PictureTagID");
            entity.Property(e => e.PictureId).HasColumnName("PictureID");
            entity.Property(e => e.PictureTagId).HasColumnName("PictureTagID");

            entity.HasOne(d => d.Picture).WithMany(p => p.PicturePictureTags)
                .HasForeignKey(d => d.PictureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Picture_PictureTag_Picture");

            entity.HasOne(d => d.PictureTag).WithMany(p => p.PicturePictureTags)
                .HasForeignKey(d => d.PictureTagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Picture_PictureTag_PictureTag");
        });

        modelBuilder.Entity<PictureTag>(entity =>
        {
            entity.ToTable("PictureTag");

            entity.Property(e => e.PictureTagId).HasColumnName("PictureTagID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Pricing>(entity =>
        {
            entity.ToTable("Pricing");

            entity.HasIndex(e => e.PricingRefundPolicy, "IX_Pricing_PricingRefundPolicy");

            entity.Property(e => e.PricingId).HasColumnName("PricingID");
            entity.Property(e => e.PriceCoefficient).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.PricingCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.PricingName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.PricingRefundPolicyNavigation).WithMany(p => p.Pricings)
                .HasForeignKey(d => d.PricingRefundPolicy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pricing_PricingRefundPolicy");
        });

        modelBuilder.Entity<PricingRefundPolicy>(entity =>
        {
            entity.ToTable("PricingRefundPolicy");

            entity.Property(e => e.PlanName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.ToTable("Review");

            entity.HasIndex(e => e.BookingId, "IX_Review_BookingID");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CustomerType).HasMaxLength(50);
            entity.Property(e => e.OwnerReponseDate).HasColumnType("datetime");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.ReviewTitle).HasMaxLength(100);

            entity.HasOne(d => d.Booking).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Review_Booking");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.HasIndex(e => e.BuildingId, "IX_Room_BuildingID");

            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.BuildingId).HasColumnName("BuildingID");
            entity.Property(e => e.RoomBedroomBedSet)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.RoomBreakfastPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RoomCreated)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.RoomDescription).IsRequired();
            entity.Property(e => e.RoomDinnerPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RoomLunchPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RoomName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.RoomPrice).HasColumnType("decimal(10, 1)");

            entity.HasOne(d => d.Building).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Room_Building");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.FackbookLoginId, "IX_User_FackbookLoginID");

            entity.HasIndex(e => e.GoogleLoginId, "IX_User_GoogleLoginID");

            entity.HasIndex(e => e.LineLoginId, "IX_User_LineLoginID");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Country).HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FackbookLoginId).HasColumnName("FackbookLoginID");
            entity.Property(e => e.GoogleLoginId).HasColumnName("GoogleLoginID");
            entity.Property(e => e.LineLoginId).HasColumnName("LineLoginID");
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserFirstName)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.UserLastName)
                .HasMaxLength(50)
                .HasDefaultValue("");

            entity.HasOne(d => d.FackbookLogin).WithMany(p => p.Users)
                .HasForeignKey(d => d.FackbookLoginId)
                .HasConstraintName("FK_User_FacebookLogin");

            entity.HasOne(d => d.GoogleLogin).WithMany(p => p.Users)
                .HasForeignKey(d => d.GoogleLoginId)
                .HasConstraintName("FK_User_GoogleLogin");

            entity.HasOne(d => d.LineLogin).WithMany(p => p.Users)
                .HasForeignKey(d => d.LineLoginId)
                .HasConstraintName("FK_User_LineLogin");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_Roles"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserRoles_User"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
