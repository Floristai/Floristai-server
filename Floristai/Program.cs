using AutoMapper;
using Floristai;
using Floristai.Dto;
using Floristai.EFContexts;
using Floristai.Entities;
using Floristai.Middleware;
using Floristai.Models;
using Floristai.Repositories;
using Floristai.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options => {options.EnableSensitiveDataLogging(); options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version())); });
builder.Services.AddControllers();


var config = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<OrderEntity, Order>();
    cfg.CreateMap<Order, OrderEntity>();
    cfg.CreateMap<OrderLine, OrderLineEntity>();
    cfg.CreateMap<OrderLineEntity, OrderLine>();
    cfg.CreateMap<FlowerEntity, Flower>();
    cfg.CreateMap<Flower, FlowerEntity>();
    cfg.CreateMap<OrderInsertDto, Order>();
    cfg.CreateMap<OrderLineInsertDto, OrderLine>();
    cfg.CreateMap<UserEntity, User>();
    cfg.CreateMap<User, UserDto>();
});
builder.Services.AddSingleton(new Mapper(config));
// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.TryAddSingleton<IJwtKeyHoldingService>(new JwtKeyHoldingService() { JwtTokenKey = builder.Configuration.GetValue<string>("JwtTokenKey:Key") });
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFlowerRepository, FlowerRepository>();
builder.Services.AddScoped<IFlowerService, FlowerService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ILoggingRepository, LoggingRepository>();
builder.Services.AddScoped<ILoggingService, LoggingService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y => {
    y.RequireHttpsMetadata = false;
    y.SaveToken = true;
    y.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JwtTokenKey:Key"))),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Policies.AdministratorOnly, policy => policy.RequireClaim(CustomClaimTypes.Administrator));
});
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().AddOData(options => options.Select().Filter());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseMiddleware<LoggingMiddleware>();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapControllers();
});


app.Run();

