using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//  .AddMicrosoftIdentityWebApi(builder.Configuration, "AzureAd");

// CORS
var myOrigins = "myOrigins";
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: myOrigins,
    policy =>
    {
      policy.WithOrigins("http://localhost:3000")
      .AllowAnyHeader()
      .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors(myOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
