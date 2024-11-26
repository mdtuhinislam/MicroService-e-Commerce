using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Persistences
{
    public class OrderContextSeed
    {
        public static async Task Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    UserName = "mdtuhinislam@gmail.com",
                    FirstName = "Md. Tuhin",
                    LastName = "Islam",
                    EmailAddress = "mdtuhinislam@gmail.com",
                    Address = "Kushtia",
                    TotalPrice = 10500,
                    City = "Kushtia",
                    PhoneNumber = "01760658575",
                    CardName="DBBL Debit Card",
                    CardNumber="25655886",
                    CreatedBy="Tuhin",
                    CreatedDate = DateTime.Now,
                    CVV = "CVV122",
                    Expiration="2026/06/01",
                    PaymentMethod = 1,
                    UpdatedAt= DateTime.Now,
                    UpdatedBy="",
                    ZipCode="1216"

                }
                );
        }
    }
}
