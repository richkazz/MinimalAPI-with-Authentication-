using Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace MinimalApi.Filters
{
    public class PostValidationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync
            (EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var post = context.GetArgument<Post>(1);
            if (string.IsNullOrEmpty(post.Content)) 
                return await Task.FromResult(Results.BadRequest("Post not valid"));

            return await next(context);
        }
    }
}
