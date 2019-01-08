using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NB.MOBILEPO.DAL.DbModels
{
    public partial class MobilePoDbContext : DbContext
    {
        public MobilePoDbContext()
        {
        }

        public MobilePoDbContext(DbContextOptions<MobilePoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gateentries> Gateentries { get; set; }
        public virtual DbSet<Purchaseorderlines> Purchaseorderlines { get; set; }
        public virtual DbSet<Purchaseorders> Purchaseorders { get; set; }
        public virtual DbSet<Receiptlines> Receiptlines { get; set; }
        public virtual DbSet<Receipts> Receipts { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Shipmentlines> Shipmentlines { get; set; }
        public virtual DbSet<Shipments> Shipments { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=mysql;database=mobilepo;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateentries>(entity =>
            {
                entity.HasKey(e => e.GetrId);

                entity.ToTable("gateentries");

                entity.HasIndex(e => e.GetrCreatedby)
                    .HasName("FK_GETR_USER_CREATEDBY_idx");

                entity.HasIndex(e => e.GetrId)
                    .HasName("GETR_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.GetrModifiedby)
                    .HasName("FK_GETR_USER_MODIFIEDBY_idx");

                entity.HasIndex(e => e.GetrNumber)
                    .HasName("GETR_NUMBER_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.GetrSpmtId)
                    .HasName("GETR_SPMT_ID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.GetrId)
                    .HasColumnName("GETR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.GetrCreatedby)
                    .HasColumnName("GETR_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.GetrCreateddate)
                    .HasColumnName("GETR_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.GetrIsdeleted)
                    .IsRequired()
                    .HasColumnName("GETR_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.GetrModifiedby)
                    .HasColumnName("GETR_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.GetrModifieddate)
                    .HasColumnName("GETR_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.GetrNumber)
                    .IsRequired()
                    .HasColumnName("GETR_NUMBER")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.GetrSpmtId)
                    .HasColumnName("GETR_SPMT_ID")
                    .HasColumnType("bigint(20)");

                entity.HasOne(d => d.GetrCreatedbyNavigation)
                    .WithMany(p => p.GateentriesGetrCreatedbyNavigation)
                    .HasForeignKey(d => d.GetrCreatedby)
                    .HasConstraintName("FK_GETR_CREATEDBY");

                entity.HasOne(d => d.GetrModifiedbyNavigation)
                    .WithMany(p => p.GateentriesGetrModifiedbyNavigation)
                    .HasForeignKey(d => d.GetrModifiedby)
                    .HasConstraintName("FK_GETR_MODIFIEDBY");

                entity.HasOne(d => d.GetrSpmt)
                    .WithOne(p => p.Gateentries)
                    .HasForeignKey<Gateentries>(d => d.GetrSpmtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GETR_SPMT_ID");
            });

            modelBuilder.Entity<Purchaseorderlines>(entity =>
            {
                entity.HasKey(e => e.ProlId);

                entity.ToTable("purchaseorderlines");

                entity.HasIndex(e => e.ProlCreatedby)
                    .HasName("FK_PROL_CREATEDBY_idx");

                entity.HasIndex(e => e.ProlId)
                    .HasName("PROL_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProlModifiedby)
                    .HasName("FK_PROL_MODIFIEDBY_idx");

                entity.HasIndex(e => e.ProlProrId)
                    .HasName("FK_PROL_PROR_ID_idx");

                entity.Property(e => e.ProlId)
                    .HasColumnName("PROL_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProlCreatedby)
                    .HasColumnName("PROL_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProlCreateddate)
                    .HasColumnName("PROL_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProlDate)
                    .HasColumnName("PROL_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProlIsdeleted)
                    .IsRequired()
                    .HasColumnName("PROL_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.ProlItem)
                    .IsRequired()
                    .HasColumnName("PROL_ITEM")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ProlItemdescription)
                    .HasColumnName("PROL_ITEMDESCRIPTION")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.ProlModifiedby)
                    .HasColumnName("PROL_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProlModifieddate)
                    .HasColumnName("PROL_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProlNumber)
                    .HasColumnName("PROL_NUMBER")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.ProlProrId)
                    .HasColumnName("PROL_PROR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProlQuantity)
                    .HasColumnName("PROL_QUANTITY")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ProlStatus)
                    .IsRequired()
                    .HasColumnName("PROL_STATUS")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.ProlCreatedbyNavigation)
                    .WithMany(p => p.PurchaseorderlinesProlCreatedbyNavigation)
                    .HasForeignKey(d => d.ProlCreatedby)
                    .HasConstraintName("FK_PROL_CREATEDBY");

                entity.HasOne(d => d.ProlModifiedbyNavigation)
                    .WithMany(p => p.PurchaseorderlinesProlModifiedbyNavigation)
                    .HasForeignKey(d => d.ProlModifiedby)
                    .HasConstraintName("FK_PROL_MODIFIEDBY");

                entity.HasOne(d => d.ProlPror)
                    .WithMany(p => p.Purchaseorderlines)
                    .HasForeignKey(d => d.ProlProrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROL_PROR_ID");
            });

            modelBuilder.Entity<Purchaseorders>(entity =>
            {
                entity.HasKey(e => e.ProrId);

                entity.ToTable("purchaseorders");

                entity.HasIndex(e => e.ProrId)
                    .HasName("PROR_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProrModifiedby)
                    .HasName("FK_PROR_USER_ID_idx");

                entity.HasIndex(e => e.ProrNumber)
                    .HasName("PROR_NUMBER_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProrSplrId)
                    .HasName("FK_PROR_SPLR_ID_idx");

                entity.Property(e => e.ProrId)
                    .HasColumnName("PROR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProrCreatedby)
                    .HasColumnName("PROR_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProrCreateddate)
                    .HasColumnName("PROR_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProrDate)
                    .HasColumnName("PROR_DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProrIsdeleted)
                    .IsRequired()
                    .HasColumnName("PROR_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.ProrModifiedby)
                    .HasColumnName("PROR_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProrModifieddate)
                    .HasColumnName("PROR_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProrNumber)
                    .IsRequired()
                    .HasColumnName("PROR_NUMBER")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ProrSplrId)
                    .HasColumnName("PROR_SPLR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.ProrStatus)
                    .IsRequired()
                    .HasColumnName("PROR_STATUS")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.ProrModifiedbyNavigation)
                    .WithMany(p => p.Purchaseorders)
                    .HasForeignKey(d => d.ProrModifiedby)
                    .HasConstraintName("FK_PROR_MODIFIEDBY");

                entity.HasOne(d => d.ProrSplr)
                    .WithMany(p => p.Purchaseorders)
                    .HasForeignKey(d => d.ProrSplrId)
                    .HasConstraintName("FK_PROR_SPLR_ID");
            });

            modelBuilder.Entity<Receiptlines>(entity =>
            {
                entity.HasKey(e => e.RctlId);

                entity.ToTable("receiptlines");

                entity.HasIndex(e => e.RctlCreatedby)
                    .HasName("FK_RCTL_USER_CREATEDBY_idx");

                entity.HasIndex(e => e.RctlId)
                    .HasName("RCTL_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RctlModifiedby)
                    .HasName("FK_RCTL_USER_MODIFIEDBY_idx");

                entity.HasIndex(e => e.RctlProlId)
                    .HasName("FK_RCTL_PROL_ID_idx");

                entity.HasIndex(e => e.RctlRcptId)
                    .HasName("FK_RCTL_RCPT_ID_idx");

                entity.Property(e => e.RctlId)
                    .HasColumnName("RCTL_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RctlCreatedby)
                    .HasColumnName("RCTL_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RctlCreateddate)
                    .HasColumnName("RCTL_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.RctlIsdeleted)
                    .IsRequired()
                    .HasColumnName("RCTL_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.RctlModifiedby)
                    .HasColumnName("RCTL_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RctlModifieddate)
                    .HasColumnName("RCTL_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.RctlProlId)
                    .HasColumnName("RCTL_PROL_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RctlQuantity)
                    .HasColumnName("RCTL_QUANTITY")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RctlRcptId)
                    .HasColumnName("RCTL_RCPT_ID")
                    .HasColumnType("bigint(20)");

                entity.HasOne(d => d.RctlCreatedbyNavigation)
                    .WithMany(p => p.ReceiptlinesRctlCreatedbyNavigation)
                    .HasForeignKey(d => d.RctlCreatedby)
                    .HasConstraintName("FK_RCTL_USER_CREATEDBY");

                entity.HasOne(d => d.RctlModifiedbyNavigation)
                    .WithMany(p => p.ReceiptlinesRctlModifiedbyNavigation)
                    .HasForeignKey(d => d.RctlModifiedby)
                    .HasConstraintName("FK_RCTL_USER_MODIFIEDBY");

                entity.HasOne(d => d.RctlProl)
                    .WithMany(p => p.Receiptlines)
                    .HasForeignKey(d => d.RctlProlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RCTL_PROL_ID");

                entity.HasOne(d => d.RctlRcpt)
                    .WithMany(p => p.Receiptlines)
                    .HasForeignKey(d => d.RctlRcptId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RCTL_RCPT_ID");
            });

            modelBuilder.Entity<Receipts>(entity =>
            {
                entity.HasKey(e => e.RcptId);

                entity.ToTable("receipts");

                entity.HasIndex(e => e.RcptCreatedby)
                    .HasName("FK_RCPT_USER_CREATEDBY_idx");

                entity.HasIndex(e => e.RcptGetrId)
                    .HasName("RCPT_GETR_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RcptId)
                    .HasName("RCPT_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RcptModifiedby)
                    .HasName("FK_RCPT_USER_MODIFIEDBY_idx");

                entity.HasIndex(e => e.RcptProrId)
                    .HasName("FK_RCPT_PROR_ID_idx");

                entity.Property(e => e.RcptId)
                    .HasColumnName("RCPT_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RcptCreatedby)
                    .HasColumnName("RCPT_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RcptCreateddate)
                    .HasColumnName("RCPT_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.RcptGetrId)
                    .HasColumnName("RCPT_GETR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RcptIsdeleted)
                    .IsRequired()
                    .HasColumnName("RCPT_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.RcptModifiedby)
                    .HasColumnName("RCPT_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RcptModifieddate)
                    .HasColumnName("RCPT_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.RcptProrId)
                    .HasColumnName("RCPT_PROR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.RcptReceiptno)
                    .IsRequired()
                    .HasColumnName("RCPT_RECEIPTNO")
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.RcptCreatedbyNavigation)
                    .WithMany(p => p.ReceiptsRcptCreatedbyNavigation)
                    .HasForeignKey(d => d.RcptCreatedby)
                    .HasConstraintName("FK_RCPT_USER_CREATEDBY");

                entity.HasOne(d => d.RcptGetr)
                    .WithOne(p => p.Receipts)
                    .HasForeignKey<Receipts>(d => d.RcptGetrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RCPT_GETR_ID");

                entity.HasOne(d => d.RcptModifiedbyNavigation)
                    .WithMany(p => p.ReceiptsRcptModifiedbyNavigation)
                    .HasForeignKey(d => d.RcptModifiedby)
                    .HasConstraintName("FK_RCPT_USER_MODIFIEDBY");

                entity.HasOne(d => d.RcptPror)
                    .WithMany(p => p.Receipts)
                    .HasForeignKey(d => d.RcptProrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RCPT_PROR_ID");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("roles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("ROLE_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.RoleRank)
                    .HasName("ROLE_RANK_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnName("ROLE_ID")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.RoleDescription)
                    .HasColumnName("ROLE_DESCRIPTION")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("ROLE_NAME")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.RoleRank)
                    .HasColumnName("ROLE_RANK")
                    .HasColumnType("smallint(6)");
            });

            modelBuilder.Entity<Shipmentlines>(entity =>
            {
                entity.HasKey(e => e.SpmlId);

                entity.ToTable("shipmentlines");

                entity.HasIndex(e => e.SpmlCreatedby)
                    .HasName("FK_SPML_CREATEDBY_idx");

                entity.HasIndex(e => e.SpmlId)
                    .HasName("SPML_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SpmlModifiedby)
                    .HasName("FK_SPML_MODIFIEDBY_idx");

                entity.HasIndex(e => e.SpmlProlId)
                    .HasName("FK_SPML_PROL_ID_idx");

                entity.HasIndex(e => e.SpmlSpmtId)
                    .HasName("FK_SPML_SPMT_ID_idx");

                entity.Property(e => e.SpmlId)
                    .HasColumnName("SPML_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmlCreatedby)
                    .HasColumnName("SPML_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmlCreateddate)
                    .HasColumnName("SPML_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.SpmlIsdeleted)
                    .IsRequired()
                    .HasColumnName("SPML_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.SpmlModifiedby)
                    .HasColumnName("SPML_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmlModifieddate)
                    .HasColumnName("SPML_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.SpmlProlId)
                    .HasColumnName("SPML_PROL_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmlQuantity)
                    .HasColumnName("SPML_QUANTITY")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SpmlSpmtId)
                    .HasColumnName("SPML_SPMT_ID")
                    .HasColumnType("bigint(20)");

                entity.HasOne(d => d.SpmlCreatedbyNavigation)
                    .WithMany(p => p.ShipmentlinesSpmlCreatedbyNavigation)
                    .HasForeignKey(d => d.SpmlCreatedby)
                    .HasConstraintName("FK_SPML_CREATEDBY");

                entity.HasOne(d => d.SpmlModifiedbyNavigation)
                    .WithMany(p => p.ShipmentlinesSpmlModifiedbyNavigation)
                    .HasForeignKey(d => d.SpmlModifiedby)
                    .HasConstraintName("FK_SPML_MODIFIEDBY");

                entity.HasOne(d => d.SpmlProl)
                    .WithMany(p => p.Shipmentlines)
                    .HasForeignKey(d => d.SpmlProlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SPML_PROL_ID");

                entity.HasOne(d => d.SpmlSpmt)
                    .WithMany(p => p.Shipmentlines)
                    .HasForeignKey(d => d.SpmlSpmtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SPML_SPMT_ID");
            });

            modelBuilder.Entity<Shipments>(entity =>
            {
                entity.HasKey(e => e.SpmtId);

                entity.ToTable("shipments");

                entity.HasIndex(e => e.SpmtCreatedby)
                    .HasName("FK_SPMT_USER_CREATEDBY_idx");

                entity.HasIndex(e => e.SpmtId)
                    .HasName("SPMT_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SpmtInvoiceno)
                    .HasName("SPMT_INVOICENO_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SpmtModifiedby)
                    .HasName("FK_SPMT_USER_MODIFIEDBY_idx");

                entity.HasIndex(e => e.SpmtProrId)
                    .HasName("FK_SPMT_PROR_ID_idx");

                entity.Property(e => e.SpmtId)
                    .HasColumnName("SPMT_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmtCreatedby)
                    .HasColumnName("SPMT_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmtCreateddate)
                    .HasColumnName("SPMT_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.SpmtInvoiceno)
                    .IsRequired()
                    .HasColumnName("SPMT_INVOICENO")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SpmtIsdeleted)
                    .IsRequired()
                    .HasColumnName("SPMT_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.SpmtLorryno)
                    .IsRequired()
                    .HasColumnName("SPMT_LORRYNO")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SpmtModifiedby)
                    .HasColumnName("SPMT_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmtModifieddate)
                    .HasColumnName("SPMT_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.SpmtProrId)
                    .HasColumnName("SPMT_PROR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SpmtShipmentdate)
                    .HasColumnName("SPMT_SHIPMENTDATE")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.SpmtCreatedbyNavigation)
                    .WithMany(p => p.ShipmentsSpmtCreatedbyNavigation)
                    .HasForeignKey(d => d.SpmtCreatedby)
                    .HasConstraintName("FK_SPMT_CREATEDBY");

                entity.HasOne(d => d.SpmtModifiedbyNavigation)
                    .WithMany(p => p.ShipmentsSpmtModifiedbyNavigation)
                    .HasForeignKey(d => d.SpmtModifiedby)
                    .HasConstraintName("FK_SPMT_MODIFIEDBY");

                entity.HasOne(d => d.SpmtPror)
                    .WithMany(p => p.Shipments)
                    .HasForeignKey(d => d.SpmtProrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SPMT_PROR_ID");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.HasKey(e => e.SplrId);

                entity.ToTable("suppliers");

                entity.HasIndex(e => e.SplrCreatedby)
                    .HasName("FK_SPLR_USER_CREATEDBY_idx");

                entity.HasIndex(e => e.SplrId)
                    .HasName("SPLR_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SplrModifiedby)
                    .HasName("FK_SPLR_USER_MODIFIEDBY_idx");

                entity.Property(e => e.SplrId)
                    .HasColumnName("SPLR_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SplrAddress)
                    .HasColumnName("SPLR_ADDRESS")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.SplrCode)
                    .IsRequired()
                    .HasColumnName("SPLR_CODE")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SplrCreatedby)
                    .HasColumnName("SPLR_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SplrCreateddate)
                    .HasColumnName("SPLR_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.SplrIsdeleted)
                    .IsRequired()
                    .HasColumnName("SPLR_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.SplrModifiedby)
                    .HasColumnName("SPLR_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.SplrModifieddate)
                    .HasColumnName("SPLR_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.SplrName)
                    .IsRequired()
                    .HasColumnName("SPLR_NAME")
                    .HasColumnType("varchar(200)");

                entity.HasOne(d => d.SplrCreatedbyNavigation)
                    .WithMany(p => p.SuppliersSplrCreatedbyNavigation)
                    .HasForeignKey(d => d.SplrCreatedby)
                    .HasConstraintName("FK_SPLR_CREATEDBY");

                entity.HasOne(d => d.SplrModifiedbyNavigation)
                    .WithMany(p => p.SuppliersSplrModifiedbyNavigation)
                    .HasForeignKey(d => d.SplrModifiedby)
                    .HasConstraintName("FK_SPLR_MODIFIEDBY");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("users");

                entity.HasIndex(e => e.UserCreatedby)
                    .HasName("FK_USER_CREATEDBY_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("USER_ID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserModifiedby)
                    .HasName("FK_USER_MODIFIEDBY_idx");

                entity.HasIndex(e => e.UserRoleId)
                    .HasName("FK_USER_ROLE_ID_idx");

                entity.HasIndex(e => e.UserSplrId)
                    .HasName("FK_USER_SPLR_ID_idx");

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserAddress)
                    .HasColumnName("USER_ADDRESS")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.UserCreatedby)
                    .HasColumnName("USER_CREATEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserCreateddate)
                    .HasColumnName("USER_CREATEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .HasColumnName("USER_EMAIL")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UserFirstname)
                    .IsRequired()
                    .HasColumnName("USER_FIRSTNAME")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UserIsdeleted)
                    .IsRequired()
                    .HasColumnName("USER_ISDELETED")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.UserLastname)
                    .IsRequired()
                    .HasColumnName("USER_LASTNAME")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UserModifiedby)
                    .HasColumnName("USER_MODIFIEDBY")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserModifieddate)
                    .HasColumnName("USER_MODIFIEDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("USER_NAME")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("USER_PASSWORD")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UserPhonenumber)
                    .HasColumnName("USER_PHONENUMBER")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.UserRoleId)
                    .HasColumnName("USER_ROLE_ID")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.UserSecretkey)
                    .IsRequired()
                    .HasColumnName("USER_SECRETKEY")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.UserSplrId)
                    .HasColumnName("USER_SPLR_ID")
                    .HasColumnType("bigint(20)");

                entity.HasOne(d => d.UserCreatedbyNavigation)
                    .WithMany(p => p.InverseUserCreatedbyNavigation)
                    .HasForeignKey(d => d.UserCreatedby)
                    .HasConstraintName("FK_USER_CREATEDBY");

                entity.HasOne(d => d.UserModifiedbyNavigation)
                    .WithMany(p => p.InverseUserModifiedbyNavigation)
                    .HasForeignKey(d => d.UserModifiedby)
                    .HasConstraintName("FK_USER_MODIFIEDBY");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USER_ROLE_ID");

                entity.HasOne(d => d.UserSplr)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserSplrId)
                    .HasConstraintName("FK_USER_SPLR_ID");
            });
        }
    }
}
