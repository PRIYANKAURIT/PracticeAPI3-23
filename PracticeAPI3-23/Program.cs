using PracticeAPI3_23.Repository;
using PracticeAPI3_23.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();
builder.Services.AddScoped<IMasterDropdownRepository, MasterDropdownRepository>();
builder.Services.AddScoped<ISchoolTypeRepository,SchoolTypeRepository>();
builder.Services.AddScoped<ISchoolCategoryRepository, SchoolCategoryRepository>();
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
