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
                        UserName = "Traveller1",
                        Email = travellerGmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newTraveller, "Coding@1234?");
                    await userManager.AddToRoleAsync(newTraveller, UserType.Traveller.ToString());
                }

                var ownerEmail = "owner1@gmail.com";
                var ownerUser = await userManager.FindByEmailAsync(ownerEmail);
                if (ownerUser == null)
                {
                    var newOwner = new User()
                    {
                        UserName = "Owner1",
                        Email = ownerEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newOwner, "@Test1234");
                    await userManager.AddToRoleAsync(newOwner, UserType.Owner.ToString());
                }
                
                var agentEmail = "agent1@gmail.com";
                var agentUser = await userManager.FindByEmailAsync(agentEmail);
                if (agentUser == null)
                {
                    var newAgent = new User()
                    {
                        UserName = "Agent1",
                        Email = agentEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAgent, "#Test4321");
                    await userManager.AddToRoleAsync(newAgent, UserType.Agent.ToString());
                }
            }
        }
}