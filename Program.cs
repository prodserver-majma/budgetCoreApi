using DinkToPdf;
using DinkToPdf.Contracts;
using mahadalzahrawebapi.Mappings;
using mahadalzahrawebapi.Models;
using mahadalzahrawebapi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddRazorPages();

Console.WriteLine("Hello, World!");

builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "");

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://mahadalzahra.org",
                                             "http://localhost:4200",
                                             "https://localhost:4200",
                                             "http://localhost:50387",
                                             "http://localhost:50118",
                                             "http://localhost:51258",
                                             "http://localhost:4201",
                                             "http://localhost:59511",
                                             "https://localhost:59511",
                                             "http://10.20.90.25:4200",
                                             "http://80.93.25.20/",
                                             "https://80.93.25.20/",
                                             "https://budget.majmamarkazi.com",
                                             "https://erp.majmamarkazi.com",
                                             "https://www.mahadalzahra.org",
                                             "https://students.mahadalzahra.org",
                                             "https://demostudents.mahadalzahra.org",
                                             "https://demo.mahadalzahra.org",
                                             "https://branch.mahadalzahra.org",
                                             "https://ec2-3-109-179-58.ap-south-1.compute.amazonaws.com")
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

// builder.Services.AddControllers()
//         .AddJsonOptions(options =>
//         {
//           options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//           options.JsonSerializerOptions.MaxDepth = 64; // Increase depth if necessary
//         });

//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false, // Set to true if you want to validate the issuer
//        ValidateAudience = false, // Set to true if you want to validate the audience
//        ClockSkew = TimeSpan.FromHours(1) // You can adjust the clock skew if needed
//    };
//});

builder.Services.AddSingleton<TokenService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true; // optional
}); ;
builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Docs", Version = "v1" });

    // Define the OAuth2.0 scheme that's in use (JWT bearer flow)
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //{
    //    Description = "JWT Authorization header using the Bearer scheme.",
    //    Name = "Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer"
    //});

    //// Apply the security globally
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        Array.Empty<string>()
    //    }
    //});
});

//builder.Services.AddDbContext<MzApiDbContext>(_ =>
//    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

string connectionString;
string planetscaleconnectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("Default");
    planetscaleconnectionString = builder.Configuration.GetConnectionString("PlanetScale");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("Default");
    planetscaleconnectionString = builder.Configuration.GetConnectionString("PlanetScale");
}

builder.Services.AddDbContext<mzdbContext>(options =>
{
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(5, 7, 17)),
        mySqlOptions => mySqlOptions
            .EnableRetryOnFailure(
                maxRetryCount: 5, // Maximum number of retries
                maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
                errorNumbersToAdd: null // MySQL error codes to consider as transient
            )
            .CommandTimeout(600)); // Increase the command timeout
});

//builder.Services.AddDbContext<mzPlanetScaleContext>(options =>
//{
//    options.UseMySql(planetscaleconnectionString,
//        new MySqlServerVersion(new Version(8,0,0)));
//});

// Explicitly specifying the assembly containing the profiles

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseSwagger(c =>
{
    c.RouteTemplate = "apidocs/swagger/{documentname}/swagger.json";
});


app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/apidocs/swagger/v1/swagger.json", "My Cool API V1");
    c.RoutePrefix = "apidocs/swagger";
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();