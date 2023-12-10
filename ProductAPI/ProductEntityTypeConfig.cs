using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductLib;

namespace ProductApi;

public class ProductEntityTypeConfig : IEntityTypeConfiguration<Coffe>
{
    public void Configure(EntityTypeBuilder<Coffe> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Code).IsUnique(true);

        builder.Property(x => x.Id)
            .IsRequired(true)
            .HasColumnName(nameof(Coffe.Id))
            .HasColumnType("varchar")
            .HasMaxLength(36)
            .IsUnicode(false)
            ;
        builder.Property(x => x.Code)
            .IsRequired(true)
            .HasColumnName(nameof(Coffe.Code))
            .HasColumnType("nvarchar")
            .HasMaxLength(20)
            .IsUnicode(true)
            ;
        builder.Property(x => x.Name)
            .IsRequired(false)
            .HasColumnName(nameof(Coffe.Name))
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsUnicode(true)
            ;
        builder.Property(x => x.Category)
            .IsRequired(true)
            .HasColumnName(nameof(Coffe.Category))
            .HasColumnType("tinyint")
            ;
        builder.Property(x => x.Price)
           .HasColumnName(nameof(Coffe.Price))
           .HasColumnType("float")
           ;
        builder.Property(x => x.CreatedOn)
            .IsRequired(false)
            .HasColumnName(nameof(Coffe.CreatedOn))
            .HasColumnType("datetime")
            ;
    }
}
