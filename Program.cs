using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQLClientAPI.Consumer;
using GraphQLClientAPI.Middleware;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();

//Single Server Memory Cache
builder.Services.AddMemoryCache();
// Redis Caching

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = configuration.GetConnectionString("Redis");
    options.InstanceName = "localRedis_";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGraphQLClient>(s=> new GraphQLHttpClient(configuration.GetConnectionString("GraphQLURI"), new NewtonsoftJsonSerializer()));
builder.Services.AddScoped<SuperHeroConsumer>();
var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseMyMiddleware();
app.Run();
