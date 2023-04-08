using Back.Auth;
using Market.ConfigLib.Entities;
using Market.ConfigLib.Interfaces;
using Market.ConfigLib.Logic;
using Market.IdentetyServer.Entities;
using Market.IdentetyServer.Logic;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



string direcoty = System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
direcoty = direcoty.Substring(6);
IConfigLoader configLoader = new FileLoader(Path.Combine(direcoty, "Config.cfg"));

GlobalConfig globalConfig = configLoader.Load<GlobalConfig>() as GlobalConfig;

 
 

builder.Services.AddDbContext<Context>();
builder.Services.AddSingleton(globalConfig);
 
 

builder.Services.AddMediatR(typeof(LoginHandler).Assembly);
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<Mediator>();
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("*")
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var builder2 = builder.Services.AddIdentityCore<IdentetyUser>();
var identityBuilder = new IdentityBuilder(builder2.UserType, builder.Services);
identityBuilder.AddEntityFrameworkStores<Context>();
identityBuilder.AddSignInManager<SignInManager<IdentetyUser>>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(globalConfig.TokenKey));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });

builder.Services.AddDbContext<Context>();
var app = builder.Build();

/////////////////////////////////////////


 
 

///////////////////////////////////////////

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();
 
 
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors("MyPolicy");


app.MapControllers();
 
 
app.Run();
 







//services.AddIdentityCore<Person>(options => { });
//new IdentityBuilder(typeof(Person), typeof(IdentityRole), services)
//    .AddRoleManager<RoleManager<IdentityRole>>()
//    .AddSignInManager<SignInManager<Person>>()
//    .AddEntityFrameworkStores<Context>()
//    .AddDefaultTokenProviders();

