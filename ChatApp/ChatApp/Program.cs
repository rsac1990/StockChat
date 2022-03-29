global using Microsoft.EntityFrameworkCore;
using ChatApp.Hubs;
using ChatApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ChatApp.MessageBroker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login";
});

//Is mandatory to login for every page excep the ones I specify its not
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddSignalR();

builder.Services.AddTransient<IRabbitMQService, RabbitMQService>();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() => RegisterSignalRWithRabbitMQ(app.Services));

void RegisterSignalRWithRabbitMQ(IServiceProvider  serviceProvider)
{
    var rabbitMQService = (IRabbitMQService)serviceProvider.GetService(typeof(IRabbitMQService));
    rabbitMQService.Connect();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.Run();
