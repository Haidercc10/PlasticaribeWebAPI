using Microsoft.EntityFrameworkCore;
using PlasticaribeAPI.Data;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dataContext>(options =>

//CONEXI�N A BASE DE DATOS PlasticaribeBDD. 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

//CONEXI�N A BASE DE DATOS ZEUS. 
builder.Services.AddDbContext<ZeusDataContext>(options =>
{    
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZeusConnection"));
});


//HABILITAR CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
    

        builder =>
        {
            
            builder.WithOrigins("http://192.168.0.153:4600")
            .AllowAnyMethod()
            .AllowAnyHeader();

        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if  (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//USAR CORS 
app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
