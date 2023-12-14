using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ParkDataLayer.Context;
using ParkDataLayer.Entitites;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ParkContext())
            {
                // Seed the database

                // Perform some operations
                ShowAllParks(context);
                
                ShowAllHousesAndContracts(context);
            }
        }

        public static void SeedDatabase(ParkContext context)
        {
            // Check if the database is already seeded
            if (!context.Parks.Any())
            {
                // Seed Parks
                var parks = new ParkEF[]
                {
                    new ParkEF { Id = "Park001", Naam = "Sunny Park", Locatie = "123 Sunny Street" },
                    new ParkEF { Id = "Park002", Naam = "Moonlight Park", Locatie = "456 Moonlight Avenue" }
                    // Add more parks as needed
                };
                context.Parks.AddRange(parks);
                context.SaveChanges();

                // Seed Houses
                var houses = new HuisEF[]
                {
                    new HuisEF { Straat = "Oak Lane", Nr = 10, Actief = true, ParkId = "Park001" },
                    new HuisEF { Straat = "Maple Street", Nr = 5, Actief = true, ParkId = "Park002" }
                    // Add more houses as needed
                };
                context.Huizen.AddRange(houses);
                context.SaveChanges();

                // Seed Tenants
                var tenants = new HuurderEF[]
                {
                    new HuurderEF { Naam = "John Doe", Telefoon = "123456789", Email = "john@example.com", Adres = "111 Grid St" },
                    new HuurderEF { Naam = "Jane Smith", Telefoon = "987654321", Email = "jane@example.com", Adres = "222 Circuit Ave" }
                    // Add more tenants as needed
                };
                context.Huurders.AddRange(tenants);
                context.SaveChanges();

                // Seed Rental Contracts
                var contracts = new HuurcontractEF[]
                {
                    new HuurcontractEF { Id = "Contract001", StartDatum = DateTime.Now, EindDatum = DateTime.Now.AddDays(10), HuisId = houses[0].Id, HuurderId = tenants[0].Id },
                    new HuurcontractEF { Id = "Contract002", StartDatum = DateTime.Now.AddDays(5), EindDatum = DateTime.Now.AddDays(15), HuisId = houses[1].Id, HuurderId = tenants[1].Id }
                    // Add more contracts as needed
                };
                context.Huurcontracten.AddRange(contracts);

                // Save changes to the database
                context.SaveChanges();
            }
        }

        private static void ShowAllParks(ParkContext context)
        {
            var parks = context.Parks.Include(p => p.Huizen).ToList();
            foreach (var park in parks)
            {
                Console.WriteLine($"Park: {park.Naam}, Location: {park.Locatie}");
                foreach (var huis in park.Huizen)
                {
                    Console.WriteLine($"\tHouse ID: {huis.Id}, Street: {huis.Straat}, Number: {huis.Nr}, Active: {huis.Actief}");
                }
            }
        }
        
        private static void ShowAllHousesAndContracts(ParkContext context)
        {
            // Fetch all houses with their contracts
            var houses = context.Huizen
                .Include(h => h.Huurcontracten)
                .ToList();

            if (houses.Any())
            {
                foreach (var house in houses)
                {
                    Console.WriteLine($"House ID: {house.Id}, Street: {house.Straat}, Number: {house.Nr}, Active: {house.Actief}");
                    foreach (var contract in house.Huurcontracten)
                    {
                        Console.WriteLine($"\tContract ID: {contract.Id}, Start Date: {contract.StartDatum}, End Date: {contract.EindDatum}");
                    }
                }
            }
            else
            {
                Console.WriteLine("No houses found.");
            }
        }
        
    }
}
