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

            var clothing = new ItemType { Name = "Clothing" };
            dbContext.Database.Insert(clothing);

            var travel = new ItemType { Name = "Travel" };
            dbContext.Database.Insert(travel);

            var food = new ItemType { Name = "Food" };
            dbContext.Database.Insert(food);

            var electricals = new ItemType { Name = "Electricals" };
            dbContext.Database.Insert(electricals);

            var utils = new ItemType { Name = "Utilities" };
            dbContext.Database.Insert(utils);

            var furniture = new ItemType { Name = "Furniture" };
            dbContext.Database.Insert(furniture);

            var activities = new ItemType { Name = "Activities" };
            dbContext.Database.Insert(activities);

            var tech = new ItemType { Name = "Technology" };
            dbContext.Database.Insert(tech);

            dbContext.Database.Insert(new Item { ItemName = "Text Message", ItemType = tech.Id, CarbonDioxideEquivalent = 0.014 });
            dbContext.Database.Insert(new Item { ItemName = "1 pint of tap water", ItemType = food.Id, CarbonDioxideEquivalent = 0.14 });
            dbContext.Database.Insert(new Item { ItemName = "1 pint of tap water poured away", ItemType = food.Id, CarbonDioxideEquivalent = 0.5 });
            dbContext.Database.Insert(new Item { ItemName = "1 web search", ItemType = tech.Id, CarbonDioxideEquivalent = 0.9 });
            dbContext.Database.Insert(new Item { ItemName = "1 spam email", ItemType = tech.Id, CarbonDioxideEquivalent = 0.3 });
            dbContext.Database.Insert(new Item { ItemName = "1 intended email", ItemType = tech.Id, CarbonDioxideEquivalent = 4 });
            dbContext.Database.Insert(new Item { ItemName = "1 intended email plus a long attachment", ItemType = tech.Id, CarbonDioxideEquivalent = 50 });
            dbContext.Database.Insert(new Item { ItemName = "1 lightweight plastic bag", ItemType = utils.Id, CarbonDioxideEquivalent = 3 });
            dbContext.Database.Insert(new Item { ItemName = "1 standard disposable plastic bag", ItemType = utils.Id, CarbonDioxideEquivalent = 10 });
            dbContext.Database.Insert(new Item { ItemName = "1 reusable plastic bag", ItemType = utils.Id, CarbonDioxideEquivalent = 50 });
        }
    }
}
