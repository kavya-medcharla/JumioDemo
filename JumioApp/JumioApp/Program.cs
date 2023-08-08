using IdentityModel.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAccessTokenManagement(options =>
{
    var tokenReq = new ClientCredentialsTokenRequest
    {
        Address = @"https://auth.amer-1.jumio.ai/oauth2/token",
        //ClientId = "9e12b4ac-3f7b-489e-ba88-b65bc31de693",
        //ClientSecret = "ErOo0Fr0VKVxtkN1P36JBSQjN1s0KJFN"
    };
    tokenReq.Headers.Authorization = new BasicAuthenticationHeaderValue("9e12b4ac-3f7b-489e-ba88-b65bc31de693", "ErOo0Fr0VKVxtkN1P36JBSQjN1s0KJFN");
    options.Client.Clients.Add("jumio", tokenReq);
});
builder.Services.AddClientAccessTokenHttpClient("jumio", configureClient: client =>
{
    client.BaseAddress = new Uri("https://account.amer-1.jumio.ai/api/v1/");
});
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

