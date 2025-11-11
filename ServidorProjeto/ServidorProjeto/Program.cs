using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Data;
using ServidorProjeto.Repositories;
using ServidorProjeto.Repositories.Interfaces;
using SQLitePCL;
using MercadoPago.Config;
using ServidorProjeto.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Inicializa SQLite
Batteries.Init();

// Configura MercadoPago
builder.Services.Configure<MercadoPagoSettings>(
    builder.Configuration.GetSection("MercadoPago")
);
// função que configura o cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var mercadoPagoToken = builder.Configuration["MercadoPago:AccessToken"]
                     ?? Environment.GetEnvironmentVariable("MERCADOPAGO_ACCESS_TOKEN");

if (!string.IsNullOrEmpty(mercadoPagoToken))
    MercadoPagoConfig.AccessToken = mercadoPagoToken;
else
    Console.WriteLine("⚠️ AccessToken do Mercado Pago NÃO configurado.");

// Controllers + JSON
builder.Services.AddControllers().AddNewtonsoftJson();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Banco SQLite
builder.Services.AddDbContext<SistemaBD>(options =>
    options.UseSqlite("Data Source=sistema.db"));

// Repositórios
builder.Services.AddScoped<IUsuarioRepo, UsuarioRepo>();
builder.Services.AddScoped<IArquivoPdfRepo, ArquivoPdfRepo>();
builder.Services.AddScoped<ITransparenciaRepo, TransparenciaRepo>();
builder.Services.AddScoped<IAtividadeRepo, AtividadeRepo>();
builder.Services.AddScoped<ITipoAtividadeRepo, TipoAtividadeRepo>();
builder.Services.AddScoped<IOuvidoriaRepo, OuvidoriaRepo>();
builder.Services.AddScoped<IContatoRepo, ContatoRepo>();


// permite o CORS
var app = builder.Build();
app.UseCors("MyAllowSpecificOrigins");


// ✅ Garante pasta wwwroot/uploads (sem IF, sem erro)
var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
Directory.CreateDirectory(uploadDir);
Console.WriteLine("📂 Pasta 'uploads' pronta para uso.");


// ✅ Aplicar migrations sem quebrar se já existir
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SistemaBD>();

    try
    {
        context.Database.Migrate();
        Console.WriteLine("✅ Migrations aplicadas!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("⚠️ Banco já atualizado ou erro ignorado: " + ex.Message);
    }
}


// Static files
app.UseStaticFiles();

// Swagger no Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("🚀 API online!");
app.Run();
