using MediatR;
using Void.Chef.Application.Common.Models;
using Void.Chef.Application.Products.Commands.CreateProduct;
using Void.Chef.Application.Products.Commands.DeleteProduct;
using Void.Chef.Application.Products.Commands.UpdateProduct;
using Void.Chef.Application.Products.Queries.GetProductsWithPagination;
using Void.Chef.Web.Infrastructure;

namespace Void.Chef.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetProductsWithPagination)
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}");
    }

    public Task<PaginatedList<ProductBriefDto>> GetProductsWithPagination(ISender sender, [AsParameters] GetProductsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateProduct(ISender sender, CreateProductCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));
        return Results.NoContent();
    }
}
