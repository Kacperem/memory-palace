using MemoryPalaceAPI;
using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Seeders;
using MemoryPalaceAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;
using FluentValidation;
using MemoryPalaceAPI.Middleware;
using MemoryPalaceAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using MemoryPalaceAPI.Models.AccountModels;
using MemoryPalaceAPI.Models.AccountModels.Validators;
using MemoryPalaceAPI.Models.TwoDigitSystemModels;
using MemoryPalaceAPI.Models.TwoDigitSystemModels.Validators;
using MemoryPalaceAPI.Models.UserModels;
using MemoryPalaceAPI.Models.UserModels.Validators;
using Microsoft.OpenApi.Models;
using MemoryPalaceAPI.Mappings;
using Microsoft.Extensions.Options;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using MemoryPalaceAPI.Swagger.Examples;

var builder = WebApplication.CreateBuilder(args);

//Add Secrets
builder.Configuration.AddJsonFile("secrets.json", optional: true);
var secrets = new Secrets();
builder.Configuration.GetSection("SecretKeys").Bind(secrets);
builder.Services.AddSingleton(secrets);

//Authentication and authorization
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);



builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateTwoDigitSystemDtoExample>();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Memory Palace API",
        Version = "v1",
        Description = "Welcome to the Memory Palace REST API, built with ASP.NET Core. " +
        "This is the backend component of the MemoryPalace application, " +
        "which is used to manage users and two-digit number systems. " +
        "Let me know if you find any bugs. Have fun (:",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    //Adds documantation to swagger 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    option.ExampleFilters();

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    option.AddSecurityDefinition("API-KEY", new OpenApiSecurityScheme
    {
        Name = "API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme",
        In = ParameterLocation.Header,
        Description = "ApiKey must appear in header"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "API-KEY"
                },
                In = ParameterLocation.Header
            },
            new string[]{}
        }
    });
});

builder.Services.AddDbContext<MemoryPalaceDbContext>();
builder.Services.AddScoped<MemoryPalaceSeeder>();

builder.Services.AddScoped<MemoryPalaceMappingService>();

builder.Services.AddScoped<ITwoDigitSystemService, TwoDigitSystemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<CreateTwoDigitSystemDto>, CreateTwoDigitSystemDtoValidator>();
builder.Services.AddScoped<IValidator<TwoDigitSystemQuery>, TwoDigitSystemQueryValidator>();
builder.Services.AddScoped<IValidator<UserQuery>, UserQueryValdiator>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthorizationHandler, TwoDigitSystemRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, UserRequirementHandler>();

builder.Services.AddScoped<ApiKeyMiddleware>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddScoped<SwaggerBasicAuthMiddleware>();

//cors policy
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173",
                                              "http://www.example.com")
														.AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

var app = builder.Build();

var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<MemoryPalaceSeeder>();

seeder.Seed();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseMiddleware<RequestTimeMiddleware>();

app.UseMiddleware<SwaggerBasicAuthMiddleware>();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseSwaggerAuthorized();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecureSwagger v1"));


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseMiddleware<ApiKeyMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
