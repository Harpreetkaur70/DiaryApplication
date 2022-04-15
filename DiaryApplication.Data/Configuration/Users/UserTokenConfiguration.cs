using DiaryApplicationAPI.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApplication.Data.Configuration.Users
{
   public class UserTokenConfiguration : IEntityTypeConfiguration<UserTokenEntity>
    {
        public void Configure(EntityTypeBuilder<UserTokenEntity> builder)
        {
            builder.Property(t => t.RecordVersion).IsConcurrencyToken().IsRowVersion().IsRequired();
            builder.ToTable("UserTokens");
        }
    }
}
