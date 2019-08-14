using Umbraco.Core;
using Umbraco.Web;
using Our.Umbraco.GraphQL;
using Our.Umbraco.GraphQL.Web;
using Umbraco.Core.Persistence;
using Website.Data.Models;

namespace Website.App_Start
{
    public class UmbracoEvents : ApplicationEventHandler
    {
        protected override void ApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UmbracoDefaultOwinStartup.MiddlewareConfigured += (sender, e) => e.AppBuilder.UseUmbracoGraphQL(applicationContext, new GraphQLServerOptions()
            {
                EnableMetrics = true,
                Debug = true
            });
        }

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var dbContext = ApplicationContext.Current.DatabaseContext;
            var db = new DatabaseSchemaHelper(dbContext.Database, ApplicationContext.Current.ProfilingLogger.Logger, dbContext.SqlSyntax);

            if (!db.TableExist("ItemTypes"))
            {
                db.CreateTable<ItemType>();
            }

            if (!db.TableExist("Items"))
            {
                db.CreateTable<Item>();
            }

            if (!db.TableExist("Variants"))
            {
                db.CreateTable<Variant>();
            }

            #region clothing

            var clothingName = "Clothing";
            var hasClothing = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", clothingName);
            if (hasClothing.Count == 0)
            {
                var clothing = new ItemType { Name = clothingName };
                dbContext.Database.Insert(clothing);
            }

            #endregion

            #region travel

            var travelName = "Travel";
            var hasTravel = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", travelName);
            if (hasTravel.Count == 0)
            {
                var travel = new ItemType { Name = travelName };
                dbContext.Database.Insert(travel);
            }

            #endregion

            #region food

            var foodName = "Food";
            var hasFood = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", foodName);
            if (hasFood.Count == 0)
            {
                var food = new ItemType { Name = foodName };
                dbContext.Database.Insert(food);

                var tapWater = new Item { ItemName = "1 pint of water", ItemType = food.Id, MinCarbonDioxideEquivalent = 0.14, MaxCarbonDioxideEquivalent = 0.5 };
                dbContext.Database.Insert(tapWater);
                dbContext.Database.Insert(new Variant { VariantName = "1 pint of tap water", Item = tapWater.Id, CarbonDioxideEquivalent = 0.14 });
                dbContext.Database.Insert(new Variant { VariantName = "1 pint of tap water poured away", Item = tapWater.Id, CarbonDioxideEquivalent = 0.5 });

                var apple = new Item { ItemName = "1 apple", ItemType = food.Id, MaxCarbonDioxideEquivalent = 150, MinCarbonDioxideEquivalent = 0 };
                dbContext.Database.Insert(apple);
                dbContext.Database.Insert(new Variant { VariantName = "1 apple from the garden", Item = apple.Id, CarbonDioxideEquivalent = 0 });
                dbContext.Database.Insert(new Variant { VariantName = "1 apple local and seasonal", Item = apple.Id, CarbonDioxideEquivalent = 10 });
                dbContext.Database.Insert(new Variant { VariantName = "1 average apple", Item = apple.Id, CarbonDioxideEquivalent = 80 });
                dbContext.Database.Insert(new Variant { VariantName = "1 shipped, cold stored and inefficiently produced", Item = apple.Id, CarbonDioxideEquivalent = 150 });

                var banana = new Item { ItemName = "1 banana", ItemType = food.Id, MinCarbonDioxideEquivalent = 80 };
                dbContext.Database.Insert(banana);

                var teaCoffee = new Item { ItemName = "1 mug of tea or coffee", ItemType = food.Id, MinCarbonDioxideEquivalent = 21, MaxCarbonDioxideEquivalent = 340 };
                dbContext.Database.Insert(teaCoffee);
                dbContext.Database.Insert(new Variant { VariantName = "1 mug of black tea or coffee, boiling only what you need", Item = teaCoffee.Id, CarbonDioxideEquivalent = 21 });
                dbContext.Database.Insert(new Variant { VariantName = "1 mug of tea or coffee with milk, boiling only what you need", Item = teaCoffee.Id, CarbonDioxideEquivalent = 53 });
                dbContext.Database.Insert(new Variant { VariantName = "1 mug of tea or coffee with milk, boiling double the water what you need", Item = teaCoffee.Id, CarbonDioxideEquivalent = 71 });
                dbContext.Database.Insert(new Variant { VariantName = "1 large cappuccino", Item = teaCoffee.Id, CarbonDioxideEquivalent = 235 });
                dbContext.Database.Insert(new Variant { VariantName = "1 large latte", Item = teaCoffee.Id, CarbonDioxideEquivalent = 340 });

                var strawberries = new Item { ItemName = "1 punnet of strawberries", ItemType = food.Id, MinCarbonDioxideEquivalent = 150, MaxCarbonDioxideEquivalent = 1800 };
                dbContext.Database.Insert(strawberries);
                dbContext.Database.Insert(new Variant { VariantName = "1 punnet of strawberries grown in season in your own country", Item = strawberries.Id, CarbonDioxideEquivalent = 150 });
                dbContext.Database.Insert(new Variant { VariantName = "1 punnet of strawberries grown out of season and flown in", Item = strawberries.Id, CarbonDioxideEquivalent = 1800 });

                var beer = new Item { ItemName = "1 pint of beer", ItemType = food.Id, MinCarbonDioxideEquivalent = 300, MaxCarbonDioxideEquivalent = 900 };
                dbContext.Database.Insert(beer);
                dbContext.Database.Insert(new Variant { VariantName = "1 pint of locally brewed cask ale at the pub", Item = beer.Id, CarbonDioxideEquivalent = 300 });
                dbContext.Database.Insert(new Variant { VariantName = "1 pint of local bottled beer from the shop", Item = beer.Id, CarbonDioxideEquivalent = 500 });
                dbContext.Database.Insert(new Variant { VariantName = "1 pint of foreign beer at the pub", Item = beer.Id, CarbonDioxideEquivalent = 500 });
                dbContext.Database.Insert(new Variant { VariantName = "1 pint of bottled beer from the shop, extensively transported", Item = beer.Id, CarbonDioxideEquivalent = 900 });
            }

            #endregion

            #region electricals

            var elecName = "Electricals";
            var hasElec = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", elecName);
            if (hasElec.Count == 0)
            {
                var electricals = new ItemType { Name = elecName };
                dbContext.Database.Insert(electricals);
            }

            #endregion

            #region utilities

            var utilName = "Utilities";
            var hasUtils = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", utilName);
            if (hasUtils.Count == 0)
            {
                var utils = new ItemType { Name = utilName };
                dbContext.Database.Insert(utils);

                var plasticBag = new Item { ItemName = "1 plastic bag", ItemType = utils.Id, MinCarbonDioxideEquivalent = 3, MaxCarbonDioxideEquivalent = 50 };
                dbContext.Database.Insert(plasticBag);
                dbContext.Database.Insert(new Variant { VariantName = "1 lightweight plastic bag", Item = plasticBag.Id, CarbonDioxideEquivalent = 3 });
                dbContext.Database.Insert(new Variant { VariantName = "1 standard disposable plastic bag", Item = plasticBag.Id, CarbonDioxideEquivalent = 10 });
                dbContext.Database.Insert(new Variant { VariantName = "1 reusable plastic bag", Item = plasticBag.Id, CarbonDioxideEquivalent = 50 });
            }

            #endregion

            #region furniture

            var furnName = "Furniture";
            var hasFurn = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", furnName);
            if (hasFurn.Count == 0)
            {
                var furniture = new ItemType { Name = furnName };
                dbContext.Database.Insert(furniture);
            }

            #endregion

            #region activities

            var actName = "Activities";
            var hasActs = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", actName);
            if (hasActs.Count == 0)
            {
                var activities = new ItemType { Name = actName };
                dbContext.Database.Insert(activities);
            }

            #endregion

            #region technology

            var techName = "Technology";
            var hasTech = dbContext.Database.Fetch<ItemType>("SELECT * FROM ItemTypes WHERE Name = @0", techName);
            if (hasTech.Count == 0)
            {
                var tech = new ItemType { Name = techName };
                dbContext.Database.Insert(tech);

                dbContext.Database.Insert(new Item { ItemName = "1 text message", ItemType = tech.Id, MinCarbonDioxideEquivalent = 0.014 });
                dbContext.Database.Insert(new Item { ItemName = "1 web search", ItemType = tech.Id, MinCarbonDioxideEquivalent = 0.9 });

                var email = new Item { ItemName = "1 email", ItemType = tech.Id, MinCarbonDioxideEquivalent = 0.3, MaxCarbonDioxideEquivalent = 50 };
                dbContext.Database.Insert(email);
                dbContext.Database.Insert(new Variant { VariantName = "1 spam email", Item = email.Id, CarbonDioxideEquivalent = 0.3 });
                dbContext.Database.Insert(new Variant { VariantName = "1 intended email", Item = email.Id, CarbonDioxideEquivalent = 4 });
                dbContext.Database.Insert(new Variant { VariantName = "1 intended email plus a long attachment", Item = email.Id, CarbonDioxideEquivalent = 50 });

                var tv = new Item { ItemName = "1 hour of TV", ItemType = tech.Id, MinCarbonDioxideEquivalent = 34, MaxCarbonDioxideEquivalent = 220 };
                dbContext.Database.Insert(tv);
                dbContext.Database.Insert(new Variant { VariantName = "1 hour of 15-inch LCD flat screen", Item = tv.Id, CarbonDioxideEquivalent = 34 });
                dbContext.Database.Insert(new Variant { VariantName = "1 hour of 28-inch CRT TV", Item = tv.Id, CarbonDioxideEquivalent = 76 });
                dbContext.Database.Insert(new Variant { VariantName = "1 hour of 32-inch LCD flat screen", Item = tv.Id, CarbonDioxideEquivalent = 88 });
                dbContext.Database.Insert(new Variant { VariantName = "1 hour of 42-inch plasma screen", Item = tv.Id, CarbonDioxideEquivalent = 220 });
            }

            #endregion
           
        }
    }
}
