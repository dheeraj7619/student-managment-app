using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentDetailsInDigitalPlatform.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(a => "The Feild is required");
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddDbContextPool<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection"));
});
builder.Services.AddScoped<IStudentRepositary, SQlStudentRepositary>();
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
{
    option.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<AppDBContext>()
    .AddDefaultTokenProviders();
//builder.Services.AddCors(option =>
//{
//    option.AddPolicy("AllOrigin", p =>
//    {
//        p.WithOrigins("\r\nfile:///C:/projects/studentdetailsindigitalplatform/studentdetailsindigitalplatform/views/account/stylesheet.css");
//    });
//});

builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseStatusCodePagesWithRedirects("/Error/{0}");
app.UseMvc();
//app.UseCors("AllOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
