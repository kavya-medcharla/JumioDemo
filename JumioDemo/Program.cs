using IdentityModel.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAccessTokenManagement(options =>
{
    var jumioTokenReq = new ClientCredentialsTokenRequest
    {
        Address = @"https://auth.amer-1.jumio.ai/oauth2/token",
        ClientId = "GetFromVault",
        ClientSecret = "GetFromVault"
    };
    options.Client.Clients.Add("jumio", jumioTokenReq);
    //options.Client.Clients.Add("axle", axleTokenReq);
});
builder.Services.AddClientAccessTokenHttpClient("jumio", configureClient: client =>
{
    client.BaseAddress = new Uri("https://account.amer-1.jumio.ai/api/v1/");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("Lula Technologies");
});
builder.Services.AddClientAccessTokenHttpClient("jumioRetrieval", configureClient: client =>
{
    client.BaseAddress = new Uri("https://retrieval.amer-1.jumio.ai/api/v1/");
    client.DefaultRequestHeaders.UserAgent.ParseAdd("Lula Technologies");
});
//builder.Services.AddClientAccessTokenHttpClient("axle", configureClient: client =>
//{
//    client.BaseAddress = new Uri("https://sandbox.axle.insure");
//});
builder.Services.AddHttpClient("axle", client =>
{
    client.BaseAddress = new Uri("https://sandbox.axle.insure/");
    client.DefaultRequestHeaders.Add("x-client-id", "GetFromVault");
    client.DefaultRequestHeaders.Add("x-client-secret", "GetFromVault");
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

