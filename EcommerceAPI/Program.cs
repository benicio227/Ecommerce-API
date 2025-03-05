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


builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 39)),
        b => b.MigrationsAssembly("EcommerceAPI.Infrastructure")
    )
);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 39))));

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


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://0.0.0.0:8080");
