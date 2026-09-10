using LMS.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddCoreServices(configuration);

var app = builder.Build();
app.BuildCoreApp();
app.Run();
