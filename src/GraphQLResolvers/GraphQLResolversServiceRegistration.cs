using GraphQLResolvers.Mutations;
using GraphQLResolvers.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLResolvers
{
    public static class GraphQLResolversServiceRegistration
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddType(new UuidType('D'))
                .AddQueryType(q => q.Name("Query"))
                .AddType<CustomerQueryResolver>()
                .AddMutationType(m => m.Name("Mutation"))
                .AddType<CustomerMutationResolver>();            

            return services;
        }
    }
}
