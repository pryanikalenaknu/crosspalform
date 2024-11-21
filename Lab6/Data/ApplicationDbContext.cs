using Lab6.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<CustomerOrder> CustomerOrders { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentStore> DepartmentStores { get; set; }
    public DbSet<DepartmentStoreChain> DepartmentStoreChains { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }
    public DbSet<RefJobTitle> RefJobTitles { get; set; }
    public DbSet<RefOrderStatusCode> RefOrderStatusCodes { get; set; }
    public DbSet<RefPaymentMethod> RefPaymentMethods { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<StaffDepartmentAssignment> StaffDepartmentAssignments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierAddress> SupplierAddresses { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>().HasKey(s => s.SupplierId);
        modelBuilder.Entity<SupplierAddress>().HasKey(sa => new { sa.SupplierId, sa.AddressId });
        modelBuilder.Entity<Address>().HasKey(a => a.AddressId);
        modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
        modelBuilder.Entity<CustomerAddress>().HasKey(ca => new { ca.CustomerId, ca.AddressId });
        modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
        modelBuilder.Entity<ProductSupplier>().HasKey(ps => new { ps.ProductId, ps.SupplierId });
        modelBuilder.Entity<CustomerOrder>().HasKey(co => co.OrderId);
        modelBuilder.Entity<OrderItem>().HasKey(oi => oi.OrderItemId);
        modelBuilder.Entity<Staff>().HasKey(st => st.StaffId);
        modelBuilder.Entity<StaffDepartmentAssignment>().HasKey(sda => new { sda.StaffId, sda.DepartmentId });
        modelBuilder.Entity<Department>().HasKey(d => d.DepartmentId);
        modelBuilder.Entity<DepartmentStore>().HasKey(ds => ds.DeptStoreId);
        modelBuilder.Entity<DepartmentStoreChain>().HasKey(dsc => dsc.DeptStoreChainId);
        modelBuilder.Entity<RefPaymentMethod>().HasKey(rpm => rpm.PaymentMethodCode);
        modelBuilder.Entity<RefOrderStatusCode>().HasKey(rosc => rosc.OrderStatusCode);
        modelBuilder.Entity<RefJobTitle>().HasKey(rjt => rjt.JobTitleCode);
        modelBuilder.Entity<RefProductType>().HasKey(rpt => rpt.ProductTypeCode);
        modelBuilder.Entity<RefPaymentMethod>().HasKey(r => r.PaymentMethodCode);
        modelBuilder.Entity<RefOrderStatusCode>().HasKey(r => r.OrderStatusCode);
        modelBuilder.Entity<RefProductType>().HasKey(r => r.ProductTypeCode);

        modelBuilder.Entity<SupplierAddress>()
            .HasOne(sa => sa.Supplier)
            .WithMany(s => s.SupplierAddresses)
            .HasForeignKey(sa => sa.SupplierId);

        modelBuilder.Entity<SupplierAddress>()
            .HasOne(sa => sa.Address)
            .WithMany(a => a.SupplierAddresses)
            .HasForeignKey(sa => sa.AddressId);

        modelBuilder.Entity<ProductSupplier>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.ProductSuppliers)
            .HasForeignKey(ps => ps.ProductId);

        modelBuilder.Entity<ProductSupplier>()
            .HasOne(ps => ps.Supplier)
            .WithMany(s => s.ProductSuppliers)
            .HasForeignKey(ps => ps.SupplierId);

        modelBuilder.Entity<CustomerAddress>()
            .HasOne(ca => ca.Customer)
            .WithMany(c => c.CustomerAddresses)
            .HasForeignKey(ca => ca.CustomerId);

        modelBuilder.Entity<CustomerAddress>()
            .HasOne(ca => ca.Address)
            .WithMany(a => a.CustomerAddresses)
            .HasForeignKey(ca => ca.AddressId);

        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.Customer)
            .WithMany(c => c.CustomerOrders)
            .HasForeignKey(co => co.CustomerId);

        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.PaymentMethod)
            .WithMany(rpm => rpm.CustomerOrders)
            .HasForeignKey(co => co.PaymentMethodCode);

        modelBuilder.Entity<CustomerOrder>()
            .HasOne(co => co.OrderStatus)
            .WithMany(rosc => rosc.CustomerOrders)
            .HasForeignKey(co => co.OrderStatusCode);
        
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.CustomerOrder)
            .WithMany(co => co.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId);

        modelBuilder.Entity<Staff>()
            .HasOne(st => st.JobTitle)
            .WithMany(rjt => rjt.Staff)
            .HasForeignKey(st => st.JobTitleCode);

        modelBuilder.Entity<Staff>()
            .HasOne(st => st.DepartmentStore)
            .WithMany(ds => ds.Staff)
            .HasForeignKey(st => st.DeptStoreId);

        modelBuilder.Entity<StaffDepartmentAssignment>()
            .HasOne(sda => sda.Staff)
            .WithMany(st => st.StaffDepartmentAssignments)
            .HasForeignKey(sda => sda.StaffId);

        modelBuilder.Entity<StaffDepartmentAssignment>()
            .HasOne(sda => sda.Department)
            .WithMany(d => d.StaffDepartmentAssignments)
            .HasForeignKey(sda => sda.DepartmentId);

        modelBuilder.Entity<DepartmentStore>()
            .HasOne(ds => ds.DepartmentStoreChain)
            .WithMany(dsc => dsc.DepartmentStores)
            .HasForeignKey(ds => ds.DeptStoreChainId);

        modelBuilder.Entity<DepartmentStore>()
            .HasOne(ds => ds.Address)
            .WithMany(a => a.DepartmentStores)
            .HasForeignKey(ds => ds.AddressId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductType)
            .WithMany()
            .HasForeignKey(p => p.ProductTypeCode);
        

        modelBuilder.Entity<Supplier>().HasData(
            new Supplier { SupplierId = 1, SupplierName = "A", SupplierDetails = "Details" },
            new Supplier { SupplierId = 2, SupplierName = "B", SupplierDetails = "Details" }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address { AddressId = 1, AddressDetails = "Kyiv Ukraine" },
            new Address { AddressId = 2, AddressDetails = "Poltava Ukraine" }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, CustomerName = "Olena Kovtun", ContactDetails = "123456789" },
            new Customer { CustomerId = 2, CustomerName = "Oksana Kovtun", ContactDetails = "987654321" }
        );

        modelBuilder.Entity<RefPaymentMethod>().HasData(
            new RefPaymentMethod { PaymentMethodCode = 1, PaymentMethodDescription = "Card" },
            new RefPaymentMethod { PaymentMethodCode = 2, PaymentMethodDescription = "Cash" }
        );

        modelBuilder.Entity<RefOrderStatusCode>().HasData(
            new RefOrderStatusCode { OrderStatusCode = 1, OrderStatusDescription = "Pending" },
            new RefOrderStatusCode { OrderStatusCode = 2, OrderStatusDescription = "Completed" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, ProductName = "Laptop", ProductTypeCode = 1 },
            new Product { ProductId = 2, ProductName = "T-Shirt", ProductTypeCode = 2 },
            new Product { ProductId = 3, ProductName = "Hammer", ProductTypeCode = 3 }
        );

        modelBuilder.Entity<RefJobTitle>().HasData(
            new RefJobTitle { JobTitleCode = 1, JobTitleDescription = "Manager" },
            new RefJobTitle { JobTitleCode = 2, JobTitleDescription = "Sales Associate" }
        );

        modelBuilder.Entity<RefProductType>().HasData(
            new RefProductType { ProductTypeCode = 1, ProductTypeDescription = "Electronics" },
            new RefProductType { ProductTypeCode = 2, ProductTypeDescription = "Clothing" },
            new RefProductType { ProductTypeCode = 3, ProductTypeDescription = "Hardware" }
        );
        
        modelBuilder.Entity<CustomerOrder>().HasData(
            new CustomerOrder
            {
                OrderId = 1, CustomerId = 1, OrderDate = DateTime.UtcNow.AddDays(-10), OrderDetails = "Details",
                OrderStatusCode = 2, PaymentMethodCode = 2
            },
            new CustomerOrder
            {
                OrderId = 2, CustomerId = 2, OrderDate = DateTime.UtcNow.AddDays(-5), OrderDetails = "Details",
                OrderStatusCode = 1, PaymentMethodCode = 1
            },
            new CustomerOrder
            {
                OrderId = 3, CustomerId = 2, OrderDate = DateTime.UtcNow.AddDays(-2), OrderDetails = "Details",
                OrderStatusCode = 2, PaymentMethodCode = 1
            }
        );
        
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem
            {
                OrderId = 1, OrderItemId = 1, ProductId = 1, Quantity = 1
            },
            new OrderItem
            {
                OrderId = 2, OrderItemId = 2, ProductId = 2, Quantity = 1
            },
            new OrderItem
            {
                OrderId = 3, OrderItemId = 3, ProductId = 2, Quantity = 1
            }
        );
    }
}