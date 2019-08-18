using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Conversion;
using GraphQL.Types;
using Our.Umbraco.GraphQL.Types;
using Our.Umbraco.GraphQL.Types.Custom;
using Our.Umbraco.GraphQL.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Website.Core.Services;

namespace Our.Umbraco.GraphQL.Schema
{
    public class UmbracoSchema : global::GraphQL.Types.Schema
    {
        // TODO: Move registration to another place
        public UmbracoSchema(
            IContentTypeService contentTypeService,
            IMemberTypeService memberTypeService,
            DatabaseContext dbContext,
            GraphQLServerOptions options)
        {
            if (contentTypeService == null)
            {
                throw new ArgumentNullException(nameof(contentTypeService));
            }

            FieldNameConverter = new DefaultFieldNameConverter();

            var documentTypes = CreateGraphTypes(contentTypeService.GetAllContentTypes(), PublishedItemType.Content).ToList();
            var mediaTypes = CreateGraphTypes(contentTypeService.GetAllMediaTypes(), PublishedItemType.Media).ToList();

            //foreach (var documentType in documentTypes.OfType<ComplexGraphType<IPublishedContent>>())
            //{
            //    var allowedChildren = documentType.GetMetadata<string[]>("allowedChildren");
            //    if (allowedChildren == null || allowedChildren.Length == 0) continue;

            //    var childTypes =
            //        documentTypes.FindAll(x =>
            //            allowedChildren.Contains(x.GetMetadata<string>("documentTypeAlias")));

            //    IGraphType childrenGraphType;
            //    if (childTypes.Count == 1)
            //    {
            //        childrenGraphType = childTypes[0];
            //    }
            //    else
            //    {
            //        var unionType = new UnionGraphType()
            //        {
            //            Name = $"{documentType.Name}Children",
            //        };

            //        foreach (var childType in childTypes.OfType<IObjectGraphType>())
            //        {
            //            unionType.AddPossibleType(childType);
            //        }

            //        childrenGraphType = unionType;

            //        RegisterTypes(unionType);
            //    }

            //    documentType.AddField(
            //        new FieldType
            //        {
            //            Name = "children",
            //            Description = "Children of the content.",
            //            Resolver = new FuncFieldResolver<IPublishedContent, object>(context => context.Source.Children),
            //            ResolvedType = new ListGraphType(childrenGraphType)
            //        }
            //    );
            //}

            RegisterTypes(documentTypes.ToArray());
            RegisterTypes(mediaTypes.ToArray());
            // RegisterTypes(memberTypeService.GetAll().CreateGraphTypes(PublishedItemType.Member, resolveName).ToArray());

            var carbonFootprintTypes = CreateCustomGraphTypes(dbContext).ToList();
            RegisterTypes(carbonFootprintTypes.ToArray());

            Query = new UmbracoQuery(documentTypes, carbonFootprintTypes);
        }

        private IEnumerable<IGraphType> CreateCustomGraphTypes(DatabaseContext dbContext)
        {
            var graphTypes = new List<IGraphType>();
            var cats = dbContext.GetAllCategories();

            foreach (var cat in cats)
            {
                graphTypes.Add(new CarbonFootprintGraphTypeMap(dbContext, cat));
            }

            return graphTypes;
        }

        public static IEnumerable<IGraphType> CreateGraphTypes(
           IEnumerable<IContentTypeComposition> contentTypes,
           PublishedItemType publishedItemType)
        {
            var interfaceGraphTypes = new Dictionary<string, IInterfaceGraphType>();

            //TODO: Whitelist/blacklist content types

            var contentTypeList = contentTypes.ToList();
            var compositions = contentTypeList.SelectMany(x => x.ContentTypeComposition).Distinct().ToList();

            foreach (var contentType in compositions)
            {
                var graphType = new PublishedContentCompositionGraphType(contentType, publishedItemType);
                yield return graphType;
                interfaceGraphTypes.Add(contentType.Alias, graphType);
            }

            foreach (var contentType in contentTypeList.Except(compositions))
            {
                var graphType = new PublishedContentGraphType(contentType, publishedItemType, interfaceGraphTypes);
                yield return graphType;
            }
        }
    }
}
