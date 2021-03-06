using GraphQL;
using GraphQL.Types;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;

namespace Our.Umbraco.GraphQL.Types
{
    public class PublishedContentGraphType : ObjectGraphType<IPublishedContent>
    {
        public PublishedContentGraphType(IContentTypeComposition contentType, PublishedItemType itemType, IDictionary<string, IInterfaceGraphType> interfaceGraphTypes)
        {
            Name = contentType.Alias.ToPascalCase();
            Description = contentType.Description;
            IsTypeOf = content => ((IPublishedContent)content).ContentType.Alias == contentType.Alias;
            Metadata = new Dictionary<string, object>
            {
                [Constants.Metadata.ContentTypeAlias] = contentType.Alias,
                [Constants.Metadata.AllowedAtRoot] = contentType.AllowedAsRoot,
                [Constants.Metadata.AllowedChildren] = contentType.AllowedContentTypes.Select(x => x.Alias).ToArray(),
            };

            Interface<PublishedContentInterfaceGraphType>();

            foreach (IContentTypeComposition composition in contentType.ContentTypeComposition)
            {
                if (interfaceGraphTypes.TryGetValue(composition.Alias, out IInterfaceGraphType interfaceType))
                {
                    AddResolvedInterface(interfaceType);
                }
            }

            this.AddUmbracoBuiltInProperties();
            this.AddUmbracoContentPropeties(contentType, itemType);
        }
    }
}
