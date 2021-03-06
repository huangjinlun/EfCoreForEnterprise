using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Sales;

namespace Store.Core.DataLayer.Mapping.Sales
{
    [Export(typeof(IEntityMap))]
    public class OrderSummaryMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderSummary>(entity =>
            {
                entity.ToTable("OrderSummary", "Sales");

                entity.HasKey(p => new { p.OrderID, p.OrderDate, p.CustomerName, p.EmployeeName, p.ShipperName, p.OrderLines });

                entity.Property(p => p.OrderID).HasColumnType("int").IsRequired();

                entity.Property(p => p.OrderDate).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.CustomerName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(p => p.EmployeeName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(p => p.ShipperName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(p => p.OrderLines).HasColumnType("int").IsRequired();
            });
        }
    }
}
