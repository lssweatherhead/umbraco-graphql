using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.ObjectResolution;

namespace Our.Umbraco.GraphQL.ValueResolvers
{
    public class GraphQLValueResolversResolver<T> : ManyObjectsResolverBase<GraphQLValueResolversResolver<T>, IGraphQLValueResolver<T>> where T:class
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private Tuple<IGraphQLValueResolver<T>, DefaultGraphQLValueResolverAttribute>[] _defaults = null;
        private IGraphQLValueResolver<T> _fallback = null;

        internal GraphQLValueResolversResolver(IServiceProvider serviceProvider, ILogger logger,
            IEnumerable<Type> value, ObjectLifetimeScope scope = ObjectLifetimeScope.Application) : base(
            serviceProvider, logger, value, scope)
        {
        }

        public IEnumerable<IGraphQLValueResolver<T>> Resolvers => Values;

        /// <summary>
        /// Caches and gets the default resolvers with their metadata
        /// </summary>
        internal Tuple<IGraphQLValueResolver<T>, DefaultGraphQLValueResolverAttribute>[] DefaultResolvers
        {
            get
            {
                using (var locker = new UpgradeableReadLock(_lock))
                {
                    if (_defaults == null)
                    {
                        locker.UpgradeToWriteLock();

                        var defaultResolverWithAttributes = Resolvers
                            .Select(x => new
                            {
                                attribute = x.GetType().GetCustomAttribute<DefaultGraphQLValueResolverAttribute>(false),
                                resolver = x
                            })
                            .Where(x => x.attribute != null)
                            .ToArray();

                        _defaults = defaultResolverWithAttributes
                            .Select(
                                x => new Tuple<IGraphQLValueResolver<T>, DefaultGraphQLValueResolverAttribute>(x.resolver, x.attribute))
                            .ToArray();
                    }

                    return _defaults;
                }
            }
        }

        /// <summary>
        /// Caches and gets the fallback resolver
        /// </summary>
        internal IGraphQLValueResolver<T> FallbackResolver
        {
            get
            {
                using (var locker = new UpgradeableReadLock(_lock))
                {
                    if (_fallback == null)
                    {
                        locker.UpgradeToWriteLock();

                        _fallback = Resolvers.Single(x => x is DefaultValueResolver);
                    }

                    return _fallback;
                }
            }
        }
    }
}
