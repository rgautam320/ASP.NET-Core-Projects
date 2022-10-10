using BookListRazor.Configs;
using BookListRazor.Model;
using BookListRazor.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoDatabase"));

BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
ConventionRegistry.Register(
    name: "CustomConventionPack",
    conventions: new ConventionPack() { new CamelCaseElementNameConvention() },
    filter: t => true);

builder.Services.AddSingleton<IBookListRepository<BookListModel>, BookListRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
