using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Repository;
using PharmcyAPI.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages();

// builder.Services.AddHttpsRedirection(options =>
// {
//     options.HttpsPort = 5176; 
// });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:3000") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("pharmaConn")));

builder.Services.AddControllers();
builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.MapControllers();
//app.MapRazorPages();

app.Run();
