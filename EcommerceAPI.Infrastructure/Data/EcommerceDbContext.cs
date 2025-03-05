namespace EcommerceAPI.Data;

using EcommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) {  }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos {  get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Carrinho> Carrinhos { get; set; }
    public DbSet<CarrinhoItem> CarrinhoItens {  get; set; }
    public DbSet<Pedido> Pedidos {  get; set; }
    public DbSet<PedidoItem> PedidoItens {  get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasKey(usuario => usuario.Id);

        modelBuilder.Entity<Usuario>()
        .Property(u => u.Id)
        .ValueGeneratedOnAdd();

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique(); // Garante que o email seja único

        // Configuração de Produto e Categoria (Relacionamento 1:N)
        modelBuilder.Entity<Produto>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuração de Carrinho e Usuario (Relacionamento 1:1)
        modelBuilder.Entity<Carrinho>()
            .HasOne(c => c.Usuario)
            .WithOne()
            .HasForeignKey<Carrinho>(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de CarrinhoItem e Produto (Relacionamento N:1)
        modelBuilder.Entity<CarrinhoItem>()
            .HasOne(ci => ci.Produto)
            .WithMany()
            .HasForeignKey(ci => ci.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de CarrinhoItem e Carrinho (Relacionamento N:1)
        modelBuilder.Entity<CarrinhoItem>()
            .HasOne(ci => ci.Carrinho)
            .WithMany(c => c.CarrinhoItems)
            .HasForeignKey(ci => ci.CarrinhoId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de Pedido e Usuario (Relacionamento 1:N)
        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de PedidoItem e Pedido (Relacionamento N:1)
        modelBuilder.Entity<PedidoItem>()
            .HasOne(pi => pi.Pedido)
            .WithMany(p => p.PedidoItems)
            .HasForeignKey(pi => pi.PedidoId)
            .HasPrincipalKey(p => p.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de PedidoItem e Produto (Relacionamento N:1)
        modelBuilder.Entity<PedidoItem>()
            .HasOne(pi => pi.Produto)
            .WithMany()
            .HasForeignKey(pi => pi.ProdutoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuração de Pagamento e Pedido (Relacionamento 1:1)
        modelBuilder.Entity<Pagamento>()
            .HasOne(p => p.Pedido)
            .WithOne()
            .HasForeignKey<Pagamento>(p => p.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

