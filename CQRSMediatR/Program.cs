using CQRSMediatR.Behaviors;
using CQRSMediatR.Exceptions;
using CQRSMediatR.Features.Products.Commands.Create;
using CQRSMediatR.Features.Products.Commands.Delete;
using CQRSMediatR.Features.Products.Commands.Update;
using CQRSMediatR.Features.Products.Queries.Get;
using CQRSMediatR.Features.Products.Queries.List;
using CQRSMediatR.Notifications;
using CQRSMediatR.Persistence;
using FluentValidation;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
    var product = await mediatr.Send(new GetProductQuery(id));
    if (product == null) return Results.NotFound();
    return Results.Ok(product);
});

app.MapGet("/products", async (ISender mediatr) =>
{
    var products = await mediatr.Send(new ListProductsQuery());
    return Results.Ok(products);
});

app.MapPost("/products", async (CreateProductCommand command, IMediator mediatr) =>
{
    var productId = await mediatr.Send(command);
    if (Guid.Empty == productId) return Results.BadRequest();
    await mediatr.Publish(new ProductCreatedNotification(productId));
    return Results.Created($"/products/{productId}", new { id = productId });
});

app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductCommand command, ISender mediatr) =>
{
    if (id != command.Id) return Results.BadRequest();
    await mediatr.Send(command);
    return Results.NoContent();
});

app.MapDelete("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
    await mediatr.Send(new DeleteProductCommand(id));
    return Results.NoContent();
});

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.Run();
