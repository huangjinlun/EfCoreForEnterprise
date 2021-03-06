using System.Composition;
using Microsoft.EntityFrameworkCore;
using Store.Core.EntityLayer.Production;

namespace Store.Core.DataLayer.Mapping.Production
{
    [Export(typeof(IEntityMap))]
    public class ProductMap : IEntityMap
    {
        public void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                // Mapping for table
                entity.ToTable("Product", "Production");

                // Set key for entity
                entity.HasKey(p => p.ProductID);

                // Set identity for entity (auto increment)
                entity.Property(p => p.ProductID).UseSqlServerIdentityColumn();

                // Set mapping for columns
                entity.Property(p => p.ProductID).HasColumnType("int").IsRequired();
                entity.Property(p => p.ProductName).HasColumnType("varchar(100)").IsRequired();
                entity.Property(p => p.ProductCategoryID).HasColumnType("int").IsRequired();
                entity.Property(p => p.UnitPrice).HasColumnType("decimal(8, 4)").IsRequired();
                entity.Property(p => p.Description).HasColumnType("varchar(255)");
                entity.Property(p => p.Discontinued).HasColumnType("bit").IsRequired();
                entity.Property(p => p.CreationUser).HasColumnType("varchar(25)").IsRequired();
                entity.Property(p => p.CreationDateTime).HasColumnType("datetime").IsRequired();
                entity.Property(p => p.LastUpdateUser).HasColumnType("varchar(25)");
                entity.Property(p => p.LastUpdateDateTime).HasColumnType("datetime");

                // Set concurrency token for entity
                entity.Property(p => p.Timestamp).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();

                // Add configuration for foreign keys
                entity
                    .HasOne(p => p.ProductCategoryFk)
                    .WithMany(b => b.Products)
                    .HasForeignKey(p => p.ProductCategoryID)
                    .HasConstraintName("fk_Product_ProductCategoryID_ProductCategory");

                // Add configuration for uniques
                entity
                    .HasAlternateKey(p => new { p.ProductName })
                    .HasName("U_ProductName");
            });
        }
    }
}
