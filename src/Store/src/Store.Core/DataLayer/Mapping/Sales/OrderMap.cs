using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Mapping.Sales
{
    [Export(typeof(IEntityMap))]
    public class OrderMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order", "Sales");

                entity.HasKey(p => p.OrderID);

                entity.Property(p => p.OrderID).UseSqlServerIdentityColumn();

                entity.Property(p => p.OrderStatusID).HasColumnType("smallint").IsRequired();
                entity.Property(p => p.OrderDate).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.CustomerID).HasColumnType("int").IsRequired();
                entity.Property(p => p.EmployeeID).HasColumnType("int");
                entity.Property(p => p.ShipperID).HasColumnType("int");
                entity.Property(p => p.Total).HasColumnType("decimal(12, 4)").IsRequired();
                entity.Property(p => p.Comments).HasColumnType("varchar(255)");
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

                entity
                    .HasOne(p => p.OrderStatusFk)
                    .WithMany(b => b.Orders)
                    .HasForeignKey(p => p.OrderStatusID)
                    .HasConstraintName("fk_Order_OrderStatusID_OrderStatus");

                entity
                    .HasOne(p => p.CustomerFk)
                    .WithMany(b => b.Orders)
                    .HasForeignKey(p => p.CustomerID)
                    .HasConstraintName("fk_Order_CustomerID_Customer");

                //entity
                //.HasOne(p => p.EmployeeFk)
                //.WithMany(b => b.Orders)
                //.HasForeignKey(p => p.EmployeeID)
                //.HasConstraintName("fk_Order_EmployeeID_Employee");

                entity
                    .HasOne(p => p.ShipperFk)
                    .WithMany(b => b.Orders)
                    .HasForeignKey(p => p.ShipperID)
                    .HasConstraintName("fk_Order_ShipperID_Shipper");
            });
        }
    }
}
