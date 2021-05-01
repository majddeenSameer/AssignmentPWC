using Microsoft.EntityFrameworkCore;
using Domain.Lookups;

namespace Infrastructure.SeedingData
{
    public static class SeedSystemData
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedRequestStatus();
            builder.SeedUserType();
            builder.SeedGender();
            builder.SeedCountry();
        }

        private static void SeedRequestStatus(this ModelBuilder builder)
        {
            builder.Entity<RequestStatus>(x =>
            {
                x.HasData(new RequestStatus
                {
                    Id = 1L,
                    DescriptionEnglish = "Approved",
                    DescriptionArabic = "Approved"
                }, new RequestStatus
                {
                    Id = 2L,
                    DescriptionEnglish = "Submitted",
                    DescriptionArabic = "Submitted"
                });
            });
        }

        private static void SeedUserType(this ModelBuilder builder)
        {
            builder.Entity<UserType>(x =>
            {
                x.HasData(new UserType
                {
                    Id = 1L,
                    DescriptionEnglish = "Admin",
                    DescriptionArabic = "Admin"
                }, new UserType
                {
                    Id = 2L,
                    DescriptionEnglish = "user",
                    DescriptionArabic = "user"
                }
               );
            });
        }

        private static void SeedGender(this ModelBuilder builder)
        {
            builder.Entity<Gender>(x =>
            {
                x.HasData(new Gender
                {
                    Id = 1L,
                    DescriptionEnglish = "male",
                    DescriptionArabic = "male"
                }, new Gender
                {
                    Id = 2L,
                    DescriptionEnglish = "female",
                    DescriptionArabic = "female"
                }
               );
            });
        }

        private static void SeedCountry(this ModelBuilder builder)
        {
            builder.Entity<Country>(x =>
            {
                x.HasData(new Country
                {
                    Id = 1L,
                    DescriptionEnglish = "Jordan",
                    DescriptionArabic = "Jordan"
                }, new Country
                {
                    Id = 2L,
                    DescriptionEnglish = "UAE",
                    DescriptionArabic = "UAE"
                }
               );
            });
        }
    }
}
