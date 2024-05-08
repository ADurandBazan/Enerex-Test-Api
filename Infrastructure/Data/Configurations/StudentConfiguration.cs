using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

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
