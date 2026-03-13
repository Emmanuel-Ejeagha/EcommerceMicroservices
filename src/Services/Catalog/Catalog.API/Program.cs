<<<<<<< HEAD
using BuildingBlocks.Exceptions.Handler;
=======
using BuildingBlocks.Behaviors;
>>>>>>> ec6ee386cc153686daf03f0f4ba2ecbad6e83045

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

<<<<<<< HEAD
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
=======
>>>>>>> ec6ee386cc153686daf03f0f4ba2ecbad6e83045

var app = builder.Build();

app.MapCarter();

<<<<<<< HEAD
app.UseExceptionHandler(options => { });

=======
>>>>>>> ec6ee386cc153686daf03f0f4ba2ecbad6e83045
app.Run();