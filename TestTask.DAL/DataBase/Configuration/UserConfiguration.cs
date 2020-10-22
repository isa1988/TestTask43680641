using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTask.Core.DataBase;

namespace TestTask.DAL.DataBase.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(200);

            builder.HasOne(p => p.Position)
                .WithMany(t => t.Users)
                .HasForeignKey(p => p.PositionId);
        }
    }
}
