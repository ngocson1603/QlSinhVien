using QlSinhVien.Data;
using Microsoft.EntityFrameworkCore;
using QlSinhVien;
using System.Reflection;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    //option.UseSqlServer("Data Source=DESKTOP-4AVR96K;Initial Catalog=QlSinhVien;Integrated Security=True;TrustServerCertificate=True");
    option.UseSqlServer("Data Source=MSI;Initial Catalog=QlSinhVien;Integrated Security=True;TrustServerCertificate=True");
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "ok",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddSwaggerGen();
var allRepositoryInterfaces = Assembly.GetAssembly(typeof(IRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();
var allRepositoryImplements = Assembly.GetAssembly(typeof(Repository<>))
    ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();
foreach (var repoType in allRepositoryInterfaces.Where(t => t.IsInterface))
{
    var implement = allRepositoryImplements.FirstOrDefault(t => t.IsClass && repoType.Name.Substring(1) == t.Name);
    if (implement != null) builder.Services.AddScoped(repoType, implement);
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
