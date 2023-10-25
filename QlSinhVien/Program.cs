using QlSinhVien.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    //option.UseSqlServer("Data Source=DESKTOP-4AVR96K;Initial Catalog=FileStore;Integrated Security=True;TrustServerCertificate=True");
    option.UseSqlServer("Data Source=MSI;Initial Catalog=FileStore;Integrated Security=True;TrustServerCertificate=True");
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
