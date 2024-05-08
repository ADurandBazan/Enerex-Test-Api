using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(t => t.Education)
                .HasMaxLength(250)
                .IsRequired();

        }
    }
}
