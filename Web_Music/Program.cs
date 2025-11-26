using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Music_Application.Interfaces;
using Music_Domain.Entities;
using Music_Infrastructure.Data;
using Music_Infrastructure.DependencyInjection;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();  
//Them MainService
builder.Services.MainService(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true; // Bắt buộc xác nhận email
})
    .AddRoles<IdentityRole>()
.AddRoleManager<RoleManager<IdentityRole>>()
.AddUserManager<UserManager<Users>>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager<SignInManager<Users>>()
.AddDefaultTokenProviders();


 

var app = builder.Build();
//Du lieu mau
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeed.DataSeedAdmin(services);
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
