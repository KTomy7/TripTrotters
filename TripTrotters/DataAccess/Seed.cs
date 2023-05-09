using Microsoft.AspNetCore.Identity;
using TripTrotters.Models;

namespace TripTrotters.DataAccess;

public class Seed
{
    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            if (!await roleManager.RoleExistsAsync(UserType.Traveller.ToString()))
                await roleManager.CreateAsync(new IdentityRole<int>(UserType.Traveller.ToString()));
            if (!await roleManager.RoleExistsAsync(UserType.Owner.ToString()))
                await roleManager.CreateAsync(new IdentityRole<int>(UserType.Owner.ToString()));
            if (!await roleManager.RoleExistsAsync(UserType.Agent.ToString()))
                await roleManager.CreateAsync(new IdentityRole<int>(UserType.Agent.ToString()));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

            var travellerGmail = "traveller1@gmail.com";
            var travellerUser = await userManager.FindByEmailAsync(travellerGmail);
            if (travellerUser == null)
            {
                var newTraveller = new User()
                {
                    UserName = "traveller1",
                    Email = travellerGmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newTraveller, "test1234");
                await userManager.AddToRoleAsync(newTraveller, UserType.Traveller.ToString());
            }

            var ownerEmail = "owner1@gmail.com";
            var ownerUser = await userManager.FindByEmailAsync(ownerEmail);
            if (ownerUser == null)
            {
                var newOwner = new User()
                {
                    UserName = "owner1",
                    Email = ownerEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newOwner, "test4321");
                await userManager.AddToRoleAsync(newOwner, UserType.Owner.ToString());
            }

            var agentEmail = "agent1@gmail.com";
            var agentUser = await userManager.FindByEmailAsync(agentEmail);
            if (agentUser == null)
            {
                var newAgent = new User()
                {
                    UserName = "agent1",
                    Email = agentEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(newAgent, "test1234");
                await userManager.AddToRoleAsync(newAgent, UserType.Agent.ToString());
            }
        }
    }

    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var context = serviceScope.ServiceProvider.GetService<TripTrottersDbContext>();

            context.Database.EnsureCreated();

            if (!context.Apartments.Any())
            {
                context.Apartments.AddRange(new List<Apartment>()
                {
                    new Apartment()
                    {
                        Title = "Apartment 1",
                        Description = "Description 1",
                        Price = 100,
                        Address = new Address
                        {
                            Country = "Romania",
                            City = "Cluj-Napoca",
                            Street = "Gheorghe Doja",
                            StreetNumber = 1
                        },
                        OwnerId = 2
                    },
                    new Apartment()
                    {
                        Title = "Apartment 2",
                        Description = "Description 2",
                        Price = 87,
                        Address = new Address
                        {
                            Country = "Romania",
                            City = "Cluj-Napoca",
                            Street = "Observator",
                            StreetNumber = 22
                        },
                        OwnerId = 2
                    },
                    new Apartment()
                    {
                        Title = "Apartment 3",
                        Description = "Description 3",
                        Price = 57,
                        Address = new Address
                        {
                            Country = "Romania",
                            City = "Cluj-Napoca",
                            Street = "Gheorghe Doja",
                            StreetNumber = 1
                        },
                        OwnerId = 2
                    }
                });
            }
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(new List<Post>()
                {
                    new Post()
                    {
                        Title = "Post 1",
                        Description = "Description 1",
                        Budget = 1000,
                        Likes = 0,
                        Date = DateTime.Now,
                        ApartmentId = 3,
                        UserId = 1
                    },
                    new Post()
                    {
                        Title = "Post 2",
                        Description = "Description 2",
                        Budget = 2020,
                        Likes = 0,
                        Date = DateTime.Now,
                        ApartmentId = 2,
                        UserId = 1
                    },
                    new Post()
                    {
                        Title = "Post 3",
                        Description = "Description 3",
                        Budget = 3500,
                        Likes = 0,
                        Date = DateTime.Now,
                        ApartmentId = 1,
                        UserId = 1
                    }
                });
            }
            if (!context.Offers.Any())
            {
                context.Offers.AddRange(new List<Offer>()
                {
                    new Offer()
                    {
                        Title = "Offer 1",
                        Description = "Description 1",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ApartmentId = 2,
                        AgentId = 3
                    },
                    new Offer()
                    {
                        Title = "Offer 2",
                        Description = "Description 2",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ApartmentId = 1,
                        AgentId = 3
                    },
                    new Offer()
                    {
                        Title = "Offer 3",
                        Description = "Description 3",
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        ApartmentId = 3,
                        AgentId = 3
                    }
                });
            }
            context.SaveChanges();
        }
    }
}