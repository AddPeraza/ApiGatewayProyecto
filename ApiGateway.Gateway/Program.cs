using ApiGateway.Gateway.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(ApiClients.SqlServer.ToString(), conf =>
    conf.BaseAddress = new Uri(builder.Configuration["Services:SqlServer"]));

builder.Services.AddHttpClient(ApiClients.PostgreSql.ToString(), conf =>
    conf.BaseAddress = new Uri(builder.Configuration["Services:PostgreSql"]));

builder.Services.AddHttpClient(ApiClients.MongoDB.ToString(), conf =>
    conf.BaseAddress = new Uri(builder.Configuration["Services:MongoDB"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
