using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Yarp.ReverseProxy;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

// Charger la configuration YARP depuis appsettings.json
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(builderContext =>
    {
        builderContext.AddPathRemovePrefix("/orders");
    });

// Authentification JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:8080/realms/OrderRealm";
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });


//builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanWriteOrders", policy =>
    {
        policy.RequireAuthenticatedUser();
    });

    options.AddPolicy("CanReadOrders", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});




var app = builder.Build();

// Middleware d'authentification
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine("Gateway hit: " + context.Request.Path);
    await next();
});
// Optionnel : bloquer toute requête sans token
/*app.Use(async (context, next) =>
{
    if (!context.User.Identity?.IsAuthenticated ?? true)
    {
        context.Response.StatusCode = 401;
        return;
    }

    await next();
});*/

// YARP : reverse proxy
app.MapReverseProxy();

app.Run();
