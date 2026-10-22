using KnittingData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


//register KnittingContext with the dependency injection container-- must be done before the app is built
var connectionString =
	builder.Configuration.GetConnectionString("Knitting")
		?? throw new InvalidOperationException("Connection string"
		+ "'Knitting' not found.");

builder.Services.AddDbContext<KnittingContext>(options =>
	options.UseSqlServer(connectionString));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	app.UseDeveloperExceptionPage();
	//app.UseMigrationsEndPoint();//this was included in the microsoft learn tutorial, but it didn't work and caused errors, so I commented it out but will leave it for now
}

//ensure there's a database to pull form if it is not found--this was also from the microsoft learn tutorial, but it didn't work. This part did not cause errors, so I will leave it for now
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<KnittingContext>();
	
	context.Database.EnsureCreated();
	//DbInitializer.Initialize(context); //this was part of the tutorial but never worked, so I commented it out, but I will leave it for now in case I can get it to work later

}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
