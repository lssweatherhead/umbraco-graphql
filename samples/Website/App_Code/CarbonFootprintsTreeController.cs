using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;

namespace Website.App_Code
{
    [Tree("carbonFootprints", "carbonFootprints", "Carbon Footprints")]
    [PluginController("carbonFootprints")]
    public class CarbonFootprintsTreeController : TreeController
    {
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = new MenuItemCollection();

            if (id == "-1")
            {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu items here following the pattern of <Umbraco.Web.Models.Trees.ActionMenuItem,umbraco.interfaces.IAction>
                menu.Items.Add<CreateChildEntity, ActionNew>(ui.Text("actions", ActionNew.Instance.Alias));
                // add refresh menu item            
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
                return menu;
            }
            // add a delete action to each individual item
            menu.Items.Add<ActionDelete>(ui.Text("actions", ActionDelete.Instance.Alias));

            return menu;
        }

        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            // check if we're rendering the root node's children
            if (id == "-1")
            {
                // you can get your custom nodes from anywhere, and they can represent anything... 
                var categories = new List<NodeItem>
                {
                    new NodeItem(1, "Clothing", "t-shirt"),
                    new NodeItem(2, "Travel", "plane"),
                    new NodeItem(3, "Food", "food"),
                    new NodeItem(4, "Electricals", "lightbulb"),
                    new NodeItem(5, "Furniture", "reception"),
                    new NodeItem(6, "Activities", "sandbox-toys")
                };

                // create our node collection
                var nodes = new TreeNodeCollection();

                // loop through our favourite things and create a tree item for each one
                foreach (var thing in categories)
                {
                    // add each node to the tree collection using the base CreateTreeNode method
                    // it has several overloads, using here unique Id of tree item, -1 is the Id of the parent node to create, eg the root of this tree is -1 by convention - the querystring collection passed into this route - the name of the tree node -  css class of icon to display for the node - and whether the item has child nodes
                    var node = CreateTreeNode(thing.Id.ToString(), thing.ParentId.ToString(), queryStrings, thing.Title, string.Format("icon-{0}", thing.Icon), false);
                    nodes.Add(node);

                }
                return nodes;
            }

            // this tree doesn't support rendering more than 1 level
            throw new NotSupportedException();
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
