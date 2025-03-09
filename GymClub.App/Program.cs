using GymClub.App.Components;
using GymClub.App.Services;
using GymClub.Database.DbModels;
using GymClub.Domain.Features.Workouts;
using GymClub.Shared;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Radzen;
using Serilog;
using Serilog.Sinks.MSSqlServer;


string outputFolderPath = AppDomain.CurrentDomain.BaseDirectory;
string LogFilePath = Path.Combine(outputFolderPath, "logs/GymClubApplication_.txt");

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json",
    optional: false,
    reloadOnChange: true).Build();

string connectionString = configuration.GetConnectionString("DbConnection")!;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(LogFilePath, rollingInterval: RollingInterval.Hour)
    .WriteTo
    .MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Tbl_LogEvents",
            AutoCreateSqlTable = true
        }
    )
    .CreateLogger();
try
{
    Log.Information("Starting the application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();
    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    builder.Services.AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    }, ServiceLifetime.Transient, ServiceLifetime.Transient);

    builder.Services.AddScoped<IInjectService, InjectService>();
    builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    builder.Services.AddScoped<DapperService>(x => new DapperService(connectionString));

    builder.Services.AddMudServices();
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddRadzenComponents();
    builder.Services.AddScoped<RegistrationService>();
    builder.Services.AddScoped<LoginService>();
    builder.Services.AddScoped<WorkoutService>();
    builder.Services.AddAuthorizationCore();
    builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve PascalCase
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    //app.UseCookieMiddleware();
    app.UseAuthorization();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "GymClubApp is terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}