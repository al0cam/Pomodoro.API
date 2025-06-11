using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });


    options.AddPolicy("AllowAllDevelopment",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


builder.Services.AddOpenApi();

builder.Services.AddDbContext<PomodorDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<ApplicationUser>() // <--- IMPORTANT CHANGE
    .AddEntityFrameworkStores<PomodorDbContext>();

builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();

builder.Services.AddScoped<ITaskItemService, TaskItemService>();

builder.Services.AddControllers();

var app = builder.Build();


app.MapOpenApi();

app.MapControllers();
app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.Run();
