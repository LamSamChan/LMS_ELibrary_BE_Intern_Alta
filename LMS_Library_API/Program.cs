using LMS_Library_API.Context;
using LMS_Library_API.Services.DepartmentService;
using LMS_Library_API.Services.RoleAccess.PermissionsService;
using LMS_Library_API.Services.RoleAccess.RoleService;
using LMS_Library_API.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Newtonsoft;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
/*builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);*/
builder.Services.AddControllers().AddDataAnnotationsLocalization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddScoped<IDepartmentSvc, DepartmentSvc>();
builder.Services.AddScoped<IPermissionsSvc, PermissionsSvc>();
builder.Services.AddScoped<IRoleSvc, RoleSvc>();
builder.Services.AddScoped<IUserSvc, UserSvc>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
