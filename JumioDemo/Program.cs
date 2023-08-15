using IdentityModel.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAccessTokenManagement(options =>
{
    var tokenReq = new ClientCredentialsTokenRequest
    {
        Address = @"https://auth.amer-1.jumio.ai/oauth2/token",
        ClientId = "5veol8gds3lr5a9p564cam90u3",
        ClientSecret = "202cgdtb1d75j2m6hel4p96bsmahq0jbk8mm6tbd76cnl10791u"
    };
    options.Client.Clients.Add("jumio", tokenReq);
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

