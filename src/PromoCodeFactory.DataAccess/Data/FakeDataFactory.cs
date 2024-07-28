using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.Core.Domain.PromoCodeManager;
using PromoCodeFactory.EntityFramework;

namespace PromoCodeFactory.DataAccess.Data
{
    public static class FakeDataFactory
    {
        public static IList<Employee> Employees => new List<Employee>()
        {
            new Employee()
            {
                Id = Guid.Parse("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"),
                Email = "owner@somemail.ru",
                FirstName = "Иван",
                LastName = "Сергеев",
                Roles = new List<Role>()
                {
                    Roles.FirstOrDefault(x => x.Name == "Admin")  
                },
                AppliedPromocodesCount = 5
            },
            new Employee()
            {
                Id = Guid.Parse("f766e2bf-340a-46ea-bff3-f1700b435895"),
                Email = "andreev@somemail.ru",
                FirstName = "Петр",
                LastName = "Андреев",
                Roles = new List<Role>()
                {
                    Roles.FirstOrDefault(x => x.Name == "PartnerManager")  
                },
                AppliedPromocodesCount = 10
            },
        };

        public static IList<Role> Roles => new List<Role>()
        {
            new Role()
            {
                Id = Guid.Parse("53729686-a368-4eeb-8bfa-cc69b6050d02"),
                Name = "Admin",
                Description = "Администратор",
            },
            new Role()
            {
                Id = Guid.Parse("b0ae7aac-5493-45cd-ad16-87426a5e7665"),
                Name = "PartnerManager",
                Description = "Партнерский менеджер"
            }
        };
        
        public static IList<Preference> Preferences => new List<Preference>()
        {
            new Preference()
            {
                Id = Guid.Parse("ef7f299f-92d7-459f-896e-078ed53ef99c"),
                Name = "Театр",
            },
            new Preference()
            {
                Id = Guid.Parse("c4bda62e-fc74-4256-a956-4760b3858cbd"),
                Name = "Семья",
            },
            new Preference()
            {
                Id = Guid.Parse("76324c47-68d2-472d-abb8-33cfa8cc0c84"),
                Name = "Дети",
            }
        };

        public static IList<Customer> Customers
        {
            get
            {
                var customerId = Guid.Parse("a6c8c6b1-4349-45b0-ab31-244740aaf0f0");
                var customers = new List<Customer>()
                {
                    new Customer()
                    {
                        Id = customerId,
                        FirstName = "Иван",
                        LastName = "Петров",
                        Email = "ivan_sergeev@mail.ru",
                        Preferences = [
                            Preferences.FirstOrDefault(p => p.Name == "Дети")
                        ],
                        PromoCodes = [
                        ]
                    }
                };

                return customers;
            }
        }

        public static IList<PromoCode> PromoCodes => new List<PromoCode>()
        {
            new PromoCode()
            {
                Id = Guid.NewGuid(),
                Code = "SALE567",
                ServiceInfo = "Special offer",
                BeginDate = new DateTime(2022, 3, 15),
                EndDate = new DateTime(2022, 5, 15),
                PartnerName = "Company B"
            },
        };
 

        public static void FillDataBase(DatabaseContext dataContext)
        {
            dataContext.Database.EnsureDeleted();
            dataContext.Database.Migrate();
                
            dataContext.Roles.AddRange(Roles);
            dataContext.Preferences.AddRange(Preferences);
            dataContext.PromoCodes.AddRange(PromoCodes);
            
            dataContext.SaveChanges();

            var testEmployees = Employees;
            testEmployees[0].Roles = new List<Role>() { dataContext.Roles.FirstOrDefault(r => r.Name == "Admin") };
            testEmployees[1].Roles = new List<Role>() { dataContext.Roles.FirstOrDefault(r => r.Name == "PartnerManager") };

            var testCustomers = Customers;
            testCustomers[0].Preferences = new List<Preference>() { dataContext.Preferences.FirstOrDefault(p => p.Name == "Дети")};
            
            dataContext.Employees.AddRange(testEmployees);
            dataContext.Customers.AddRange(testCustomers);
            dataContext.SaveChanges(); 
            
            var testPromoCode = dataContext.Set<PromoCode>().FirstOrDefault();

            testPromoCode.PartnerManager = dataContext.Set<Employee>().FirstOrDefault();
            testPromoCode.Preference = dataContext.Set<Preference>().FirstOrDefault();
            testPromoCode.Customer = dataContext.Set<Customer>().FirstOrDefault();
            
            dataContext.SaveChanges();
        }
    }
}