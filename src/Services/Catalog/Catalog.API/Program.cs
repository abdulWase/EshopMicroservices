


var builder = WebApplication.CreateBuilder(args);

//Add services to the container
builder.Services.RegisterMapsterConfiguration();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));//ValidationBehavior is generic one here
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(opts => {
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    })
    .UseLightweightSessions();//use for crud opertions it is used for performance
if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<CatalogInitialData>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
//Configure the HTTP request pipleine.
app.MapCarter();
app.UseExceptionHandler(options => { 

});
app.Run();
