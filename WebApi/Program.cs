using Application.Mapper;
using Application.Services.AuthServices;
using Application.Services.TicketServices;
using Application.Services.UserServices;
using Contracts.TicketContract;
using Contracts.UserContract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Repository;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
		options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticketing System API", Version = "v1" });

	// Add JWT Authentication to Swagger
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\""
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});
builder.Services.AddDbContext<AppDbContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetConnectionString("OfflineTicketingConnectionString")));
builder.Services.AddApplicationMappings();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<ITicket, TicketRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<TicketServices>();
builder.Services.AddScoped<JwtService>();
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSettings["SecretKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings["Issuer"],
			ValidAudience = jwtSettings["Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
		};
	});
builder.Services.AddAuthorization();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	DbInitializer.Initialize(context);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticketing System API v1");
		c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
	});
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
