using Ediplan.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ediplan.Persistence.Configurations;
internal class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {

        builder
            .HasMany(a => a.AssetGroups)
            .WithMany(a => a.Assets)
            .UsingEntity("asset_group_map");

        // Define TPC strategy and use a sequence to ensure continuity of Id field between tables.
        builder
            .UseTpcMappingStrategy()
            .Property(a => a.Id)
            //.HasDefaultValueSql("nextval('AssetIds'::regclass)") ; // Db dependant language, change for Db type.
            .HasDefaultValueSql("nextval('\"AssetIds\"')");

    }
}
