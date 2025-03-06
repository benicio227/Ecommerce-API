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



var connectionString = Environment.GetEnvironmentVariable("MYSQL_URL");

if (string.IsNullOrEmpty(connectionString))
{
    // Se a vari�vel de ambiente n�o estiver definida, usa o valor de "DefaultConnection" do appsettings.json
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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

app.Run("http://0.0.0.0:8080");
