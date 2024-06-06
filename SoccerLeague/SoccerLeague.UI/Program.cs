using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoccerLeague.Data;
using SoccerLeague.Domain;
using SoccerLeague.Domain.Contracts;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddTransient<IArenaService, ArenaService>();
builder.Services.AddTransient<ILeagueService, LeagueService>();
builder.Services.AddTransient<ITeamService, TeamService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IMatchService, MatchService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();