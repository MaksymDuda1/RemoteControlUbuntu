using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RemoteControlUbuntu.API.Middlewares;
using RemoteControlUbuntu.Application.Abstractions;
using RemoteControlUbuntu.Application.MappingProfile;
using RemoteControlUbuntu.Application.Models;
using RemoteControlUbuntu.Application.Services;
using RemoteControlUbuntu.Domain.Abstractions;
using RemoteControlUbuntu.Domain.Entities;
using RemoteControlUbuntu.Infrastructure;
using RemoteControlUbuntu.Infrastructure.Connectors;
using RemoteControlUbuntu.Infrastructure.OpenAIIntegration;
using RemoteControlUbuntu.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConnectionRepository, ConnectionRepository>();
builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddScoped<ICommandsBlackListRepository, CommandsBlackListRepository>();
builder.Services.AddScoped<ICommandsWhiteListRepository, CommandsWhiteListRepository>();
builder.Services.AddScoped<IUserCommandsWhiteListRepository, UserCommandsWhiteListRepository>();

//Services
builder.Services.AddScoped<IConnectionService, ConnectionService>();
builder.Services.AddTransient<IConnector, Connector>();
builder.Services.AddScoped<ICommandService, CommandService>();
// builder.Services.AddTransient<IExecuteCommandService, ExecuteCommandService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped(typeof(Lazy<>), typeof(LazyInstance<>));

//Infrastructure
builder.Services.AddScoped<IOpenAICaller, OpenAICaller>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<RemoteDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddDbContext<RemoteDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(RemoteDbContext))));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();