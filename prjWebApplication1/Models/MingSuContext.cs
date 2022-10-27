using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class MingSuContext : DbContext
    {
        public MingSuContext()
        {
        }

        public MingSuContext(DbContextOptions<MingSuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivityReference> ActivityReferences { get; set; }
        public virtual DbSet<Add> Adds { get; set; }
        public virtual DbSet<AddCatergory> AddCatergories { get; set; }
        public virtual DbSet<AddReference> AddReferences { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminReference> AdminReferences { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentCatergory> EquipmentCatergories { get; set; }
        public virtual DbSet<EquipmentReference> EquipmentReferences { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ImageReference> ImageReferences { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MingSu;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activity");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.ActivityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ActivityStatus)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ActivityReference>(entity =>
            {
                entity.ToTable("ActivityReference");

                entity.Property(e => e.ActivityReferenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("ActivityReferenceID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.ActivityReferences)
                    .HasForeignKey(d => d.ActivityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityReference_Activity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ActivityReferences)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityReference_Order");
            });

            modelBuilder.Entity<Add>(entity =>
            {
                entity.ToTable("Add");

                entity.Property(e => e.AddId).HasColumnName("AddID");

                entity.Property(e => e.AddCategoryId).HasColumnName("AddCategoryID");

                entity.Property(e => e.AddName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.AddPrice).HasColumnType("money");

                entity.HasOne(d => d.AddCategory)
                    .WithMany(p => p.Adds)
                    .HasForeignKey(d => d.AddCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Add_AddCatergory");
            });

            modelBuilder.Entity<AddCatergory>(entity =>
            {
                entity.ToTable("AddCatergory");

                entity.Property(e => e.AddCatergoryId).HasColumnName("AddCatergoryID");

                entity.Property(e => e.AddCatergoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AddReference>(entity =>
            {
                entity.ToTable("AddReference");

                entity.Property(e => e.AddReferenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("AddReferenceID");

                entity.Property(e => e.AddId).HasColumnName("AddID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Add)
                    .WithMany(p => p.AddReferences)
                    .HasForeignKey(d => d.AddId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AddReference_Add");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.AddReferences)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AddReference_Order");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminInfo).IsRequired();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Admins)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Admin_Member");
            });

            modelBuilder.Entity<AdminReference>(entity =>
            {
                entity.ToTable("AdminReference");

                entity.Property(e => e.AdminReferenceId)
                    .ValueGeneratedNever()
                    .HasColumnName("AdminReferenceID");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.AdminReferences)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdminReference_Admin");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.AdminReferences)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdminReference_Room");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId)
                    .ValueGeneratedNever()
                    .HasColumnName("CommentID");

                entity.Property(e => e.CommentStatus).HasMaxLength(50);

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_RoomStyle");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.RoomDiscountId);

                entity.ToTable("Discount");

                entity.Property(e => e.RoomDiscountId).HasColumnName("RoomDiscountID");

                entity.Property(e => e.DiscountInfo).IsRequired();

                entity.Property(e => e.DiscountName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.EquipmentCatergoryId).HasColumnName("EquipmentCatergoryID");

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EquipmentCatergory)
                    .WithMany(p => p.Equipment)
                    .HasForeignKey(d => d.EquipmentCatergoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Equipment_EquipmentCatergory");
            });

            modelBuilder.Entity<EquipmentCatergory>(entity =>
            {
                entity.ToTable("EquipmentCatergory");

                entity.Property(e => e.EquipmentCatergoryId).HasColumnName("EquipmentCatergoryID");

                entity.Property(e => e.EquipmentCatergoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EquipmentReference>(entity =>
            {
                entity.ToTable("EquipmentReference");

                entity.Property(e => e.EquipmentReferenceId).HasColumnName("EquipmentReferenceID");

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.EquipmentReferences)
                    .HasForeignKey(d => d.EquipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentReference_Equipment");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.EquipmentReferences)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EquipmentReference_Room");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.Image1).HasColumnName("Image");
            });

            modelBuilder.Entity<ImageReference>(entity =>
            {
                entity.ToTable("ImageReference");

                entity.Property(e => e.ImageReferenceId).HasColumnName("ImageReferenceID");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageReferences)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageReference_Image");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.ImageReferences)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageReference_Room");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Authority)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberAccount)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberPhone)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderstatusId).HasColumnName("OrderstatusID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Member");

                entity.HasOne(d => d.Orderstatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderStatus");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderDetailID");

                entity.Property(e => e.ActivityId).HasColumnName("ActivityID");

                entity.Property(e => e.OrderEndDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderPrice).HasColumnType("money");

                entity.Property(e => e.OrderStartDate).HasColumnType("datetime");

                entity.Property(e => e.Payment)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoomDiscountId).HasColumnName("RoomDiscountID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Order");

                entity.HasOne(d => d.RoomDiscount)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.RoomDiscountId)
                    .HasConstraintName("FK_OrderDetails_Discount");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.OrderstatusId).HasColumnName("OrderstatusID");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PayId);

                entity.ToTable("Payment");

                entity.Property(e => e.PayId)
                    .HasMaxLength(50)
                    .HasColumnName("PayID");

                entity.Property(e => e.Payment1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Payment");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fimage)
                    .HasColumnType("image")
                    .HasColumnName("FImage");

                entity.Property(e => e.FimagePath)
                    .HasMaxLength(200)
                    .HasColumnName("FImagePath");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.RoomIntrodution)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoomName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoomPrice).HasColumnType("money");

                entity.Property(e => e.RoomstatusId).HasColumnName("RoomstatusID");

                entity.HasOne(d => d.Roomstatus)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomstatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_RoomStatus");
            });

            modelBuilder.Entity<RoomStatus>(entity =>
            {
                entity.ToTable("RoomStatus");

                entity.Property(e => e.RoomstatusId).HasColumnName("RoomstatusID");

                entity.Property(e => e.StatusContent).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
