using PropertySystem.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PropertySystem.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(u => u.Id);

        builder.Property(user => user.FirstName)
                       .HasMaxLength(30)
            .IsUnicode(false)
            .HasConversion(firtsName => firtsName.Value, value => new FirstName(value));

        builder.Property(user => user.SecondName)
                        .HasMaxLength(30)
            .IsUnicode(false)
            .HasConversion(secondName => secondName.Value, value => new SecondName(value));

        builder.Property(user => user.LastName)
                       .HasMaxLength(30)
            .IsUnicode(false)
           .HasConversion(lastName => lastName.Value, value => new LastName(value));

        builder.Property(user => user.FullName)
                       .HasMaxLength(100)
            .IsUnicode(false)
           .HasConversion(fullName => fullName.Value, value => new FullName(value));

        builder.OwnsOne(user => user.UserContact, userContact =>
        {
            userContact.Property(c => c.Phone)
                            .HasMaxLength(15)
            .IsUnicode(false);

            userContact.Property(c => c.Address)
                             .HasMaxLength(200)
            .IsUnicode(false);
        });

        builder.Property(user => user.BusinessUnit)
         .HasConversion(businessUnit => businessUnit.Value, value => new BusinessUnit(value));

        builder.Property(user => user.Password)
                        .HasMaxLength(50)
            .IsUnicode(false)
         .HasConversion(password => password.Value, value => new Password(value));

        builder.Property(user => user.Role)
    .HasConversion(roleId => roleId.Value, value => new Role(value))
    .IsRequired();

        builder.Property(user => user.Identification)
         .HasMaxLength(50)
      .HasConversion(identification => identification.Value, value => new Identification(value));

        builder.Property(user => user.Email)
            .HasMaxLength(400)
            .IsUnicode(false)
            .HasConversion(email => email.Value, value => new Domain.Users.Email(value));

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasIndex(u => u.IdentityId).IsUnique().IsUnique(false);
    }
}
