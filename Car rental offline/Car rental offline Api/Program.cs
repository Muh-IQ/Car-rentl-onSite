using Business;
using Car_rental_offline_Api.Auth;
using Data;
using Data.ImplementeRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.ImplementaionRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ADD CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:3000")  //Domain accept it
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); 
    });
});

// coonect with db
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conection"),
    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

// debendecy
builder.Services.AddTransient(typeof(ILoginRepository), typeof(ImplementeLoginRepository));
builder.Services.AddTransient(typeof(LoginService), typeof(LoginService));

builder.Services.AddTransient(typeof(DbContext), typeof(AppDbContext));
builder.Services.AddTransient(typeof(IRepository<>), typeof(ImplementaionRepository<>));
builder.Services.AddTransient(typeof(RepositoryService<>), typeof(RepositoryService<>));
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddTransient(typeof(IDifferentOperationsRepository), typeof(ImplementeDifferentOperationsRepository));
builder.Services.AddTransient(typeof(DifferentOperationsService), typeof(DifferentOperationsService));

// JWT setting 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});


builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
