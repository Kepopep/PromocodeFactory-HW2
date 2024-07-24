using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PromoCodeFactory.Core.Domain.PromoCodeManager;

namespace PromoCodeFactory.EntityFramework.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id)
            .HasName("CustomerId");

        builder.Property(c => c.FirstName).HasMaxLength(15);
        builder.Property(c => c.LastName).HasMaxLength(15);
        builder.Property(c => c.Email).HasMaxLength(30);

        builder.HasMany(c => c.Preferences)
            .WithMany(p => p.Customers)
            .UsingEntity(
                "CustomerPreference",
                l => l.HasOne(typeof(Preference))
                    .WithMany()
                    .HasForeignKey("PreferencesId")
                    .HasPrincipalKey(nameof(Preference.Id))
                    .IsRequired(),
                r => r.HasOne(typeof(Customer))
                    .WithMany()
                    .HasForeignKey("CustomersId")
                    .HasPrincipalKey(nameof(Customer.Id))
                    .IsRequired(),
            j => j.HasKey("CustomersId", "PreferencesId")
                );

        /*
        builder.HasMany(c => c.PromoCodes)
            .WithOne(p => p.Customer)
            .HasForeignKey(nameof(Customer))
            .IsRequired();
        */
    }
}