using Azure.Storage.Blobs;
using LMS_Library_API.Context;
using LMS_Library_API.Helpers;
using LMS_Library_API.Helpers.BlobHelperService;
using LMS_Library_API.Services.DepartmentService;
using LMS_Library_API.Services.RoleAccess.PermissionsService;
using LMS_Library_API.Services.RoleAccess.RoleService;
using LMS_Library_API.Services.ServiceAboutUser.HelpService;
using LMS_Library_API.Services.ServiceAboutUser.PrivateFileService;
using LMS_Library_API.Services.ServiceAboutUser.QnALikesService;
using LMS_Library_API.Services.SystemInfomationService;
using LMS_Library_API.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/*builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);*/
builder.Services.AddControllers().AddDataAnnotationsLocalization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => { 
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "LMS ELibrary API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddSingleton(a => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobConnectionString")));


builder.Services.AddScoped<IDepartmentSvc, DepartmentSvc>();
builder.Services.AddScoped<IPermissionsSvc, PermissionsSvc>();
builder.Services.AddScoped<IRoleSvc, RoleSvc>();
builder.Services.AddScoped<IUserSvc, UserSvc>();
builder.Services.AddScoped<IHelpSvc, HelpSvc>();
builder.Services.AddScoped<IQnALikesSvc, QnALikesSvc>();
builder.Services.AddScoped<IPrivateFileSvc, PrivateFileSvc>();
builder.Services.AddScoped<ISystemInfomationSvc, SystemInfomationSvc>();

//Helpe
builder.Services.AddSingleton<IEncodeHelper, EncodeHelper>();
builder.Services.AddSingleton<IBlobStorageSvc, BlobStorageSvc>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "LMS ELibrary API"); });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
