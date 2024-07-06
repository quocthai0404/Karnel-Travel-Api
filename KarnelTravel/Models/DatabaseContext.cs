using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KarnelTravel.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveAccount> ActiveAccounts { get; set; }

    public virtual DbSet<AdminAccount> AdminAccounts { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Beach> Beaches { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<FlightInvoice> FlightInvoices { get; set; }

    public virtual DbSet<ForgetPassword> ForgetPasswords { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<HotelInvoice> HotelInvoices { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomHotelQuantity> RoomHotelQuantities { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<SiteType> SiteTypes { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourInvoice> TourInvoices { get; set; }

    public virtual DbSet<TourPersonQuantity> TourPersonQuantities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-6L06R65;Database=Karnel_Travel;user id=sa;password=123456;trusted_connection=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__active_a__3213E83F0A9192CB");

            entity.ToTable("active_account");

            entity.HasIndex(e => e.Email, "UQ__active_a__AB6E61641D7EB53C").IsUnique();

            entity.HasIndex(e => e.SecurityCode, "idx_security_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.SecurityCode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("security_code");
        });

        modelBuilder.Entity<AdminAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__admin_ac__3213E83FD6CA96E7");

            entity.ToTable("admin_account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__airport__C795D5160C2EE93D");

            entity.ToTable("airport");

            entity.Property(e => e.AirportId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("airport_id");
            entity.Property(e => e.AirportName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("airport_name");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
        });

        modelBuilder.Entity<Beach>(entity =>
        {
            entity.HasKey(e => e.BeachId).HasName("PK__beach__7DED2FB0C9A3697F");

            entity.ToTable("beach");

            entity.HasIndex(e => e.BeachName, "idx_beach_name");

            entity.Property(e => e.BeachId).HasColumnName("beach_id");
            entity.Property(e => e.BeachLocation)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("beach_location");
            entity.Property(e => e.BeachName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("beach_name");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.LocationId).HasColumnName("location_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Beaches)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__beach__location___6C190EBB");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__booking__5DE3A5B169FF8B8C");

            entity.ToTable("booking");

            entity.HasIndex(e => e.UserId, "idx_user_id_booking");

            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.BookingDate)
                .HasColumnType("datetime")
                .HasColumnName("booking_date");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Flight).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__booking__flight___0B91BA14");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__booking__hotel_i__0C85DE4D");

            entity.HasOne(d => d.Tour).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.TourId)
                .HasConstraintName("FK__booking__tour_id__0D7A0286");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__booking__user_id__0A9D95DB");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("discount");

            entity.HasIndex(e => e.DiscountCode, "UQ__discount__75C1F006DACF2813").IsUnique();

            entity.Property(e => e.DiscountCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("discount_code");
            entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK__district__2521322B2475358A");

            entity.ToTable("district");

            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("district_name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__district__provin__3D5E1FD2");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.FacilityId).HasName("PK__facility__B2E8EAAE9D545117");

            entity.ToTable("facility");

            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.FacilityName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("facility_name");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__flight__E3705765AC75B7F6");

            entity.ToTable("flight");

            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.ArrivalAirportId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("arrival_airport_id");
            entity.Property(e => e.ArrivalTime).HasColumnName("arrival_time");
            entity.Property(e => e.DepartureAirportId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("departure_airport_id");
            entity.Property(e => e.DepartureTime).HasColumnName("departure_time");
            entity.Property(e => e.FlightPrice).HasColumnName("flight_price");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.ArrivalAirport).WithMany(p => p.FlightArrivalAirports)
                .HasForeignKey(d => d.ArrivalAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__flight__arrival___07C12930");

            entity.HasOne(d => d.DepartureAirport).WithMany(p => p.FlightDepartureAirports)
                .HasForeignKey(d => d.DepartureAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__flight__departur__06CD04F7");
        });

        modelBuilder.Entity<FlightInvoice>(entity =>
        {
            entity.HasKey(e => e.FlightInvoiceId).HasName("PK__flight_i__54F1175F58EAEAB9");

            entity.ToTable("flight_invoice");

            entity.HasIndex(e => e.BookingId, "UQ__flight_i__5DE3A5B0230AB03A").IsUnique();

            entity.Property(e => e.FlightInvoiceId).HasColumnName("flight_invoice_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("discount_code");
            entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent");
            entity.Property(e => e.FlightPrice).HasColumnName("flight_price");
            entity.Property(e => e.NumOfPassengers).HasColumnName("num_of_passengers");
            entity.Property(e => e.SubTotal).HasColumnName("sub_total");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Booking).WithOne(p => p.FlightInvoice)
                .HasForeignKey<FlightInvoice>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__flight_in__booki__19DFD96B");
        });

        modelBuilder.Entity<ForgetPassword>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__forget_p__3213E83F334E67E8");

            entity.ToTable("forget_password");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Expire)
                .HasColumnType("datetime")
                .HasColumnName("expire");
            entity.Property(e => e.Token)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("PK__hotel__45FE7E26745412A7");

            entity.ToTable("hotel");

            entity.HasIndex(e => e.HotelName, "idx_hotel_name");

            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.HotelDescription)
                .HasColumnType("text")
                .HasColumnName("hotel_description");
            entity.Property(e => e.HotelLocation)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("hotel_location");
            entity.Property(e => e.HotelName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hotel_name");
            entity.Property(e => e.HotelPriceRange)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hotel_price_range");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.LocationId).HasColumnName("location_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__hotel__location___4D94879B");

            entity.HasMany(d => d.Facilities).WithMany(p => p.Hotels)
                .UsingEntity<Dictionary<string, object>>(
                    "HotelFacility",
                    r => r.HasOne<Facility>().WithMany()
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_hotel_facility__facility"),
                    l => l.HasOne<Hotel>().WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_hotel_facility__hotel"),
                    j =>
                    {
                        j.HasKey("HotelId", "FacilityId").HasName("PK__hotel_fa__BED0F08CBD3A681D");
                        j.ToTable("hotel_facility");
                        j.IndexerProperty<int>("HotelId").HasColumnName("hotel_id");
                        j.IndexerProperty<int>("FacilityId").HasColumnName("facility_id");
                    });
        });

        modelBuilder.Entity<HotelInvoice>(entity =>
        {
            entity.HasKey(e => e.HotelInvoiceId).HasName("PK__hotel_in__53B4F48C9B2EB024");

            entity.ToTable("hotel_invoice");

            entity.HasIndex(e => e.BookingId, "UQ__hotel_in__5DE3A5B07C9A5AB1").IsUnique();

            entity.Property(e => e.HotelInvoiceId).HasColumnName("hotel_invoice_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CheckinDate)
                .HasColumnType("datetime")
                .HasColumnName("checkin_date");
            entity.Property(e => e.CheckoutDate)
                .HasColumnType("datetime")
                .HasColumnName("checkout_date");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("discount_code");
            entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent");
            entity.Property(e => e.NumOfAdults).HasColumnName("num_of_adults");
            entity.Property(e => e.NumOfChildren).HasColumnName("num_of_children");
            entity.Property(e => e.NumOfDays).HasColumnName("num_of_days");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.RoomPrice).HasColumnName("room_price");
            entity.Property(e => e.SubTotal).HasColumnName("sub_total");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.Booking).WithOne(p => p.HotelInvoice)
                .HasForeignKey<HotelInvoice>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__hotel_inv__booki__114A936A");

            entity.HasOne(d => d.Room).WithMany(p => p.HotelInvoices)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__hotel_inv__room___123EB7A3");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__location__771831EABB1D1925");

            entity.ToTable("location");

            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.LocationNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("location_number");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.StreetId).HasColumnName("street_id");
            entity.Property(e => e.WardId).HasColumnName("ward_id");

            entity.HasOne(d => d.District).WithMany(p => p.Locations)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__location__distri__47DBAE45");

            entity.HasOne(d => d.Province).WithMany(p => p.Locations)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__location__provin__46E78A0C");

            entity.HasOne(d => d.Street).WithMany(p => p.Locations)
                .HasForeignKey(d => d.StreetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__location__street__49C3F6B7");

            entity.HasOne(d => d.Ward).WithMany(p => p.Locations)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__location__ward_i__48CFD27E");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__photo__CB48C83D6735D86B");

            entity.ToTable("photo");

            entity.HasIndex(e => e.PhotoUrl, "idx_photo_url");

            entity.Property(e => e.PhotoId).HasColumnName("photo_id");
            entity.Property(e => e.BeachId).HasColumnName("beach_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("photo_url");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.SiteId).HasColumnName("site_id");

            entity.HasOne(d => d.Beach).WithMany(p => p.Photos)
                .HasForeignKey(d => d.BeachId)
                .HasConstraintName("FK__photo__beach_id__71D1E811");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Photos)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__photo__hotel_id__6EF57B66");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Photos)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__photo__restauran__70DDC3D8");

            entity.HasOne(d => d.Room).WithMany(p => p.Photos)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__photo__room_id__6FE99F9F");

            entity.HasOne(d => d.Site).WithMany(p => p.Photos)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("FK__photo__site_id__72C60C4A");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PK__province__08DCB60F0CA9462F");

            entity.ToTable("province");

            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("province_name");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PK__restaura__3B0FAA9183DA4325");

            entity.ToTable("restaurant");

            entity.HasIndex(e => e.RestaurantName, "idx_restaurant_name");

            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.RestaurantDescription)
                .HasColumnType("text")
                .HasColumnName("restaurant_description");
            entity.Property(e => e.RestaurantLocation)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("restaurant_location");
            entity.Property(e => e.RestaurantName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("restaurant_name");
            entity.Property(e => e.RestaurantPriceRange)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("restaurant_price_range");

            entity.HasOne(d => d.Location).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__restauran__locat__5BE2A6F2");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__review__60883D902342CBA7");

            entity.ToTable("review");

            entity.HasIndex(e => e.HotelId, "idx_review_hotel");

            entity.HasIndex(e => e.UserId, "idx_review_user");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.ReviewStar).HasColumnName("review_star");
            entity.Property(e => e.ReviewText)
                .HasMaxLength(500)
                .HasColumnName("review_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__review__hotel_id__5FB337D6");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("FK__review__restaura__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__review__user_id__60A75C0F");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__room__19675A8A17465B6E");

            entity.ToTable("room");

            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.NumOfDoubleBed).HasColumnName("num_of_double_bed");
            entity.Property(e => e.NumOfSingleBed).HasColumnName("num_of_single_bed");
            entity.Property(e => e.RoomDescription)
                .HasColumnType("text")
                .HasColumnName("room_description");
            entity.Property(e => e.RoomName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("room_name");
            entity.Property(e => e.RoomPrice).HasColumnName("room_price");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__room__hotel_id__5165187F");
        });

        modelBuilder.Entity<RoomHotelQuantity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("room_hotel_quantity");

            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.QuantityLeft).HasColumnName("quantity_left");
            entity.Property(e => e.QuantityMax).HasColumnName("quantity_max");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.SiteId).HasName("PK__site__B22FDBCA4184A8D3");

            entity.ToTable("site");

            entity.HasIndex(e => e.SiteName, "idx_site_name");

            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.SiteLocation)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("site_location");
            entity.Property(e => e.SiteName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("site_name");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Sites)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__site__location_i__6754599E");

            entity.HasOne(d => d.Type).WithMany(p => p.Sites)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__site__type_id__68487DD7");
        });

        modelBuilder.Entity<SiteType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__site_typ__2C00059839633D4E");

            entity.ToTable("site_type");

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.HasKey(e => e.StreetId).HasName("PK__street__665BB66BDFEDAF89");

            entity.ToTable("street");

            entity.Property(e => e.StreetId).HasColumnName("street_id");
            entity.Property(e => e.StreetName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("street_name");
            entity.Property(e => e.WardId).HasColumnName("ward_id");

            entity.HasOne(d => d.Ward).WithMany(p => p.Streets)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__street__ward_id__4316F928");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.TourId).HasName("PK__tour__4B16B9E6D0352A8D");

            entity.ToTable("tour");

            entity.HasIndex(e => e.TourName, "idx_tour_name");

            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.Arrival).HasColumnName("arrival");
            entity.Property(e => e.Departure).HasColumnName("departure");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.TourDescription)
                .HasColumnType("text")
                .HasColumnName("tour_description");
            entity.Property(e => e.TourName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tour_name");
            entity.Property(e => e.TourPrice).HasColumnName("tour_price");

            entity.HasOne(d => d.ArrivalNavigation).WithMany(p => p.TourArrivalNavigations)
                .HasForeignKey(d => d.Arrival)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tour__arrival__778AC167");

            entity.HasOne(d => d.DepartureNavigation).WithMany(p => p.TourDepartureNavigations)
                .HasForeignKey(d => d.Departure)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tour__departure__76969D2E");
        });

        modelBuilder.Entity<TourInvoice>(entity =>
        {
            entity.HasKey(e => e.TourInvoiceId).HasName("PK__tour_inv__CDD7DA85364B66DF");

            entity.ToTable("tour_invoice");

            entity.HasIndex(e => e.BookingId, "UQ__tour_inv__5DE3A5B0F597CFEB").IsUnique();

            entity.Property(e => e.TourInvoiceId).HasColumnName("tour_invoice_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("discount_code");
            entity.Property(e => e.DiscountPercent).HasColumnName("discount_percent");
            entity.Property(e => e.NumOfPeople).HasColumnName("num_of_people");
            entity.Property(e => e.SubTotal).HasColumnName("sub_total");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.TourPrice).HasColumnName("tour_price");

            entity.HasOne(d => d.Booking).WithOne(p => p.TourInvoice)
                .HasForeignKey<TourInvoice>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tour_invo__booki__160F4887");
        });

        modelBuilder.Entity<TourPersonQuantity>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tour_person_quantity");

            entity.Property(e => e.PerLeft).HasColumnName("per_left");
            entity.Property(e => e.PerMax).HasColumnName("per_max");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370FA07CE648");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "UQ__user__AB6E61647AFDEBBB").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.IsHide).HasColumnName("is_hide");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.WardId).HasName("PK__ward__396B899DE0C129B1");

            entity.ToTable("ward");

            entity.Property(e => e.WardId).HasColumnName("ward_id");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.WardName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ward_name");

            entity.HasOne(d => d.District).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ward__district_i__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
