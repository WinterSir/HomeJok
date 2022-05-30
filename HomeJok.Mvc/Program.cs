using IdentityModel;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//¹Ø±ÕJwtÓ³Éä
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
//×¢²áÊÚÈ¨
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://login.wintersir.com";   
    options.RequireHttpsMetadata = true;               
    options.ClientId = "mvc";
    options.ClientSecret = "Hybrid";
    options.ResponseType = "code id_token";
    options.Scope.Add(OidcConstants.StandardScopes.OpenId);
    options.Scope.Add(OidcConstants.StandardScopes.Profile);
    options.Scope.Add("HomeJokScope");
    options.SaveTokens = true; //±íÊ¾TokenÒª´æ´¢
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
