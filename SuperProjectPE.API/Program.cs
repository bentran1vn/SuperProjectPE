using SuperProjectPE.API.Extensions;
using SuperProjectPE.DAO;
using SuperProjectPE.REPO.Abstract;
using SuperProjectPE.REPO.Services.Category;
using SuperProjectPE.REPO.Services.Identity;
using SuperProjectPE.REPO.Services.Jwt;
using SuperProjectPE.REPO.Services.SilverJewelry;

var builder = WebApplication.CreateBuilder(args);

// DAO
builder.Services
    .AddDbContext<SilverJewelry2023DbContext>()
    .AddTransient(typeof(IBaseDAO<>), typeof(BaseDAO<>));

//REPO
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();
builder.Services.AddTransient<IIdentityServices, IdentityService>();
builder.Services.AddTransient<ISilverJewelryService, SilverJewelryService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

//API
builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddSwaggerServices();
builder.Services.AddOdataServices();
builder.Services.AddEndpointsApiExplorer();

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
