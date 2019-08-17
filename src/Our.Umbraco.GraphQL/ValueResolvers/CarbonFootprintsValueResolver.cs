using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Our.Umbraco.GraphQL.Types.Custom;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Core.Services;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.PublishedContent;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    public class CarbonFootprintsValueResolver : GraphQLValueResolver<PublishedPropertyType>
    {
        public DatabaseContext DbContext { get; set;  }
        public DatabaseSchemaHelper DbSchemaHelper { get; set; }

        public CarbonFootprintsValueResolver() : this(ApplicationContext.Current.DatabaseContext, ApplicationContext.Current.ProfilingLogger.Logger)
        {
        }

        public CarbonFootprintsValueResolver(DatabaseContext databaseContext, ILogger logger)
        {
            DbContext = databaseContext ?? throw new ArgumentNullException(nameof(DatabaseContext));
            DbSchemaHelper = new DatabaseSchemaHelper(databaseContext.Database, logger, databaseContext.SqlSyntax);
        }

        public override Type GetGraphQLType(PublishedPropertyType propertyType)
        {
            return typeof(CarbonFootprintItemGraphType);
        }

        public override bool IsResolver(PublishedPropertyType propertyType)
        {
            return true;
        }

        public object Resolve(PublishedPropertyType propertyType, object value)
        {
            throw new NotImplementedException();
        }
    }
}
