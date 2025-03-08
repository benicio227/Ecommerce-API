using EcommerceAPI.Application.Services;
using EcommerceAPI.Application.Validators;
using EcommerceAPI.Data;
using EcommerceAPI.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProdutoCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PedidoCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PedidoItemCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PagamentoCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CarrinhoCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CarrinhoItemCreateDtoValidator>();


var environment = builder.Environment.EnvironmentName;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (environment == "Development")
{
    // Se estiver rodando local, tenta pegar da variável de ambientte local
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
    // Se estiver rodando no Railway, tenta pegar da variável de ambiente
    var railwayConnection = Environment.GetEnvironmentVariable("MYSQL_URL");
    if (!string.IsNullOrEmpty(railwayConnection))
    {
        connectionString = railwayConnection;
    }
}


builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 39)),
        b => b.MigrationsAssembly("EcommerceAPI.Infrastructure")
             .EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
    )
);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddDbContext<EcommerceDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//        new MySqlServerVersion(new Version(8, 0, 39))));

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
builder.Services.AddScoped<ICarrinhoItemRepository, CarrinhoItemRepository>();
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<IPedidoItemRepository, PedidoItemRepository>();


builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPedidoItemService, PedidoItemService>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";

//app.Run($"http://0.0.0.0:{port}");
app.Run();