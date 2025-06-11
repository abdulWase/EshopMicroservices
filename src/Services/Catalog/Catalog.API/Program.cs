using BuildingBlocks.Behaviors;
using Catalog.API;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.RegisterMapsterConfiguration();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));//ValidationBehavior is generic one here
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(opts => {
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    })
    .UseLightweightSessions();//use for crud opertions it is used for performance

var app = builder.Build();
//Configure the HTTP request pipleine.
app.MapCarter();

app.Run();
