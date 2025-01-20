using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Void.Chef.Domain.Entities;

namespace Void.Chef.Infrastructure.Data.Configurations;

public class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        builder.Property(u => u.Name)
            .HasMaxLength(100);
        
        builder.Property(u => u.PluralName)
            .HasMaxLength(100);
    }
}