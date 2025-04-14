using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Denso.HotSheet.Authorization.Roles;
using Denso.HotSheet.Authorization.Users;
using Denso.HotSheet.MultiTenancy;
using Denso.HotSheet.Catalogs;
using Denso.HotSheet.Organization;
using Denso.HotSheet.HotSheet;
using Denso.HotSheet.Email;
using Denso.HotSheet.Interfaces;
using Denso.HotSheet.Surveys;

namespace Denso.HotSheet.EntityFrameworkCore
{
    // https://aspnetboilerplate.com/Pages/Documents/Dapper-Integration

    public class HotSheetDbContext : AbpZeroDbContext<Tenant, Role, User, HotSheetDbContext>
    {
        /* Organization and Users */
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<PlantUser> UserPlants { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentUser> UserDepartments { get; set; }

        /* Catalogs */
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<CarrierService> CarrierServices { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierAddress> SupplierAddresses { get; set; }
        public virtual DbSet<SupplierBroker> SupplierBrokers { get; set; }        
        public virtual DbSet<HotSheetReason> HotSheetReasons { get; set; }
        public virtual DbSet<PartNumber> PartNumbers { get; set; }
        public virtual DbSet<PartNumberPrice> PartNumberPrices { get; set; }        
        public virtual DbSet<HotSheetTerm> HotSheetTerms { get; set; }
        public virtual DbSet<ProductCodeSAT> ProductCodesSAT { get; set; }
        public virtual DbSet<UnitMeasure> UnitMeasures { get; set; }
        public virtual DbSet<UnitMeasureSAT> UnitMeasuresSAT { get; set; }
        public virtual DbSet<RMAAssignment> RMAAssignments { get; set; }
        public virtual DbSet<PaidBy> PaidBy { get; set; }
        public virtual DbSet<PaymentTerm> PaymentTerms { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        //public virtual DbSet<StaffUser> StaffUsers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerPlant> CustomerPlants { get; set; }
        public virtual DbSet<CustomerPlantContact> CustomerPlantContacts { get; set; }        
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<DocumentStatus> DocumentStatuses { get; set; }
        public virtual DbSet<PaymentTermCarrier> PaymentTermCarriers { get; set; }
        public virtual DbSet<Packaging> Packaging { get; set; }
        public virtual DbSet<SpecialExpeditedReason> SpecialExpeditedReasons { get; set; }
        public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<EmployeeLevel> EmployeeLevels { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }

        public virtual DbSet<HelpInfo> HelpInfo { get; set; }
        public virtual DbSet<HelpInfoField> HelpInfoFields { get; set; }

        public virtual DbSet<PartNumberInternal> PartNumbersInternal { get; set; }
        public virtual DbSet<PartNumberPriceInternal> PartNumberPricesInternal { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }

        public virtual DbSet<PaidByHotSheetTerm> PaidByHotSheetTerms { get; set; }
        public virtual DbSet<PaidByPaymentTerm> PaidByPaymentTerms { get; set; }
        public virtual DbSet<CarrierNonWorkingDay> CarrierNonWorkingDays { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<Notice> Notices { get; set; }

        

        public virtual DbSet<ShortageShift> ShortageShift { get; set; }

        public virtual DbSet<StatusHotSheet> StatusHotSheet { get; set; }

        public virtual DbSet<TransportMode> DensoTransportMode { get; set; }


        /* Hot Sheet */
        public virtual DbSet<HotSheets> HotSheets { get; set; }
        public virtual DbSet<HotSheetsShip> HotSheetShip { get; set; }
        public virtual DbSet<HotSheetShipProduct> HotSheetShipProducts { get; set; }
        public virtual DbSet<CigmaExport> CigmaExports { get; set; }
        public virtual DbSet<HotSheetShipPackaging> HotSheetShipPackaging { get; set; }
        public virtual DbSet<HotSheetShipHistory> HotSheetHistory { get; set; }
        public virtual DbSet<HotSheetShipApproval> HotSheetApprovals { get; set; }
        public virtual DbSet<HotSheetShipManifest> HotSheetShipManifests { get; set; }

        public virtual DbSet<HotSheetShipManifest> HotSheetsManifest { get; set; }

       


        /* Interfaces */
        public virtual DbSet<Interface> Interfaces { get; set; }
        public virtual DbSet<InterfaceLog> InterfaceLogs { get; set; }

        public HotSheetDbContext(DbContextOptions<HotSheetDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Carrier>(s =>
            {
                s.HasMany(s => s.Services)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.DocumentType)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(s => s.NonWorkingDays)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Supplier>(s =>
            {
                s.HasMany(b => b.Addresses)
                    .WithOne(b => b.Supplier)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<HotSheets>(s =>
            {
                s.HasOne(b => b.ShortageShift)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.StatusHotSheet)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.TransportMode)
                  .WithMany()
                  .OnDelete(DeleteBehavior.Restrict);
            });


                modelBuilder.Entity<HotSheetsShip>(s =>
            {
                s.HasOne(b => b.DocumentType)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.Carrier)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.Service)
                     .WithMany()
                     .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.Reason)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.PaymentTerm)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.Plant)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.Customer)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.CustomerPlant)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(s => s.Products)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.CostPaidBy)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.FreightPaidBy)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.Department)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.Status)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(s => s.CigmaExports)
                   .WithOne(s => s.HotSheet)
                   .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(s => s.Packaging)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(s => s.History)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Customer>(s =>
            {
                s.HasMany(s => s.Plants)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Staff>(s =>
            {
                s.HasOne(b => b.User)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(s => s.CopyTo)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PaymentTerm>(s =>
            {
                s.HasMany(s => s.Carriers)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Department>(s =>
            {
                s.HasMany(s => s.Users)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Plant>(s =>
            {
                s.HasMany(s => s.Users)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<PartNumber>(s =>
            {
                s.HasMany(s => s.HotSheetShipProducts)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(s =>
            {
                s.HasMany(s => s.Departments)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasMany(s => s.Plants)
                    .WithOne()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<HotSheetShipProduct>(s =>
            {
                s.HasOne(b => b.OriginCountry)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                s.HasOne(b => b.UnitMeasure)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<HotSheetShipPackaging>(s =>
            {
                s.HasOne(b => b.Packaging)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}