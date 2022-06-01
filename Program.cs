using list_service.DBContexts;
using list_service.Messaging;
using list_service.Repositories;
using list_service.Repositories.Interfaces;
using list_service.Services;
using list_service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// set cors
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "https://planning-app.marktempelman.duckdns.org")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

// Set connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ToDoListContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
builder.Services.AddTransient<IToDoListService, ToDoListService>();
builder.Services.AddTransient<IToDoListRepository, ToDoListRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = bool.Parse(builder.Configuration["Authentication:RequireHttpsMetadata"]);
    o.Authority = builder.Configuration["Authentication:Authority"];
    o.IncludeErrorDetails = bool.Parse(builder.Configuration["Authentication:IncludeErrorDetails"]);
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = bool.Parse(builder.Configuration["Authentication:ValidateAudience"]),
        ValidAudience = builder.Configuration["Authentication:ValidAudience"],
        ValidateIssuerSigningKey = bool.Parse(builder.Configuration["Authentication:ValidateIssuerSigningKey"]),
        ValidateIssuer = bool.Parse(builder.Configuration["Authentication:ValidateIssuer"]),
        ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
        ValidateLifetime = bool.Parse(builder.Configuration["Authentication:ValidateLifetime"]),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(name: "list", pattern: "list/{controller=ToDoList}/{action=Index}/{id?}");

app.Run();

Receive.ReceiveMessage();