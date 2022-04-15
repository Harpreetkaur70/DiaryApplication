using DiaryApplicationAPI.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplication.Data.Configuration.Users
{
   public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(t => t.RecordVersion).IsConcurrencyToken().IsRowVersion().IsRequired();
            builder.ToTable("Users");
        }
    }
}
