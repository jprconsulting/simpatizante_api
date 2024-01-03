using beneficiarios_dif_api;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);


//builder.WebHost.UseWebRoot("wwwroot");
startup.ConfigureServices(builder.Services);

var app = builder.Build();
app.UseStaticFiles();

startup.Configure(app, app.Environment);
app.Run();
