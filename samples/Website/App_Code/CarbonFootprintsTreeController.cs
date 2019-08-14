using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Core.Persistence;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Website.Data.Models;

namespace Website.App_Code
{
    [Tree("carbonFootprints", "carbonFootprints", "Carbon Footprints")]
    [PluginController("carbonFootprints")]
    public class CarbonFootprintsTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (id == "-1")
            {
                menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));         
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
                return menu;
            }

            menu.Items.Add<ActionDelete>(ui.Text("actions", ActionDelete.Instance.Alias));

            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var dbContext = ApplicationContext.DatabaseContext;
            var db = dbContext.Database;

            // create our node collection
            var nodes = new TreeNodeCollection();

            // check if we're rendering the root node's children
            if (id == "-1")
            {
                var itemTypesQry = new Sql()
                        .Select("*")
                        .From<ItemType>(dbContext.SqlSyntax);

                var itemTypes = db.Fetch<ItemType>(itemTypesQry);
                var categories = itemTypes.Select(x => new NodeItem(x.Id, x.Name, GetIcon(x.Name)));

                foreach (var thing in categories)
                {
                    var node = CreateTreeNode(thing.Id.ToString(), thing.ParentId.ToString(), queryStrings, thing.Title, string.Format("icon-{0}", thing.Icon), true);
                    nodes.Add(node);
                }             
            }
            else
            {
                var parsedId = int.Parse(id);
                var itemQry = new Sql().Select("*").From<Item>(dbContext.SqlSyntax).Where<Item>(i => i.ItemType == parsedId, dbContext.SqlSyntax);
                var items = db.Fetch<Item>(itemQry);

                foreach (var item in items)
                {
                    nodes.Add(CreateTreeNode(item.Id.ToString(), id, queryStrings, item.ItemName, "icon-footprints"));
                }
            }

            return nodes;

        }

        private string GetIcon(string name)
        {
            switch (name)
            {
                case "Clothing":
                    return "t-shirt";
                case "Travel":
                    return "plane";
                case "Food":
                    return "food";
                case "Electricals":
                    return "lightbulb";
                case "Utilities":
                    return "umb-settings";
                case "Furniture":
                    return "reception";
                case "Activities":
                    return "sandbox-toys";
                default:
                    return "tv";
            }
        }
    }

    public class NodeItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }

        public string Title { get; set; }
        public string Icon { get; set; }

        public bool HasChildren { get; set; }

        public NodeItem(int id, string title, string icon = "document", int parentId = -1, bool hasChildren = false) {
            Id = id;
            Title = title;
            Icon = icon;
            ParentId = parentId;
            HasChildren = hasChildren;
        }
    }
}
