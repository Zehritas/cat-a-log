using ApexCharts;
using cat_a_logB.Areas.Identity;
using cat_a_logB.Data;
using cat_a_logB.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("cat_a_logBContextConnection") ?? throw new InvalidOperationException("Connection string 'cat_a_logBContextConnection' not found.");

builder.Services.AddDbContext<cat_a_logBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<cat_a_logBContext>();

builder.Services.AddScoped<IDependencyService, DependencyService>();
builder.Services.AddScoped<IMilestoneService, MilestoneService>();
builder.Services.AddScoped<IProjectTeamService, ProjectTeamService>();
builder.Services.AddScoped<ITaskDataService, TaskDataService>();



// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<cat_a_logB.Pages.SampleData>();
builder.Services.AddScoped<ApexChart<cat_a_logB.Data.TaskData>>();
builder.Services.AddScoped<TaskManager>();
builder.Services.AddScoped<CalculationData>();
//builder.Services.AddScoped<DependencyManager>();
builder.Services.AddScoped<MilestoneManager>();
builder.Services.AddScoped<TeamManager>();
builder.Services.AddScoped<AuthenticationStateProvider,
    RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

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

app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();