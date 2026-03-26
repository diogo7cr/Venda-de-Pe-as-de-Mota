using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotoPartsShop.Models;

namespace MotoPartsShop.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    // ========================================
    // DBSETS - Tabelas da Base de Dados
    // ========================================
    
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Modelo> Modelos { get; set; }
    public DbSet<Peca> Pecas { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    // NOVAS TABELAS - Sistema de Pedidos
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }
    public DbSet<NotificacaoStock> NotificacoesStock { get; set; }
    public DbSet<Cupao> Cupoes { get; set; } = default!;
    
    // ADICIONA ESTAS 3 NOVAS TABELAS - Sistema de Clientes e Encomendas
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Encomenda> Encomendas { get; set; }
    public DbSet<ItemEncomenda> ItensEncomenda { get; set; }

    // ========================================
    // CONFIGURAÇÃO DE RELACIONAMENTOS
    // ========================================
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ========================================
        // RELACIONAMENTOS EXISTENTES
        // ========================================

        // Relação: Peça -> Categoria
        modelBuilder.Entity<Peca>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Pecas)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relação: Peça -> Modelo
        modelBuilder.Entity<Peca>()
            .HasOne(p => p.Modelo)
            .WithMany(m => m.Pecas)
            .HasForeignKey(p => p.ModeloId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relação: Modelo -> Marca
        modelBuilder.Entity<Modelo>()
            .HasOne(m => m.Marca)
            .WithMany(ma => ma.Modelos)
            .HasForeignKey(m => m.MarcaId)
            .OnDelete(DeleteBehavior.Restrict);

        // ========================================
        // NOVOS RELACIONAMENTOS - Clientes e Encomendas
        // ========================================

        // Relação: Encomenda -> Cliente
        modelBuilder.Entity<Encomenda>()
            .HasOne(e => e.Cliente)
            .WithMany(c => c.Encomendas)
            .HasForeignKey(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relação: ItemEncomenda -> Encomenda
        modelBuilder.Entity<ItemEncomenda>()
            .HasOne(i => i.Encomenda)
            .WithMany(e => e.ItensEncomenda)
            .HasForeignKey(i => i.EncomendaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relação: ItemEncomenda -> Peça
        modelBuilder.Entity<ItemEncomenda>()
            .HasOne(i => i.Peca)
            .WithMany()
            .HasForeignKey(i => i.PecaId)
            .OnDelete(DeleteBehavior.Restrict);

        // ========================================
        // CONFIGURAÇÕES ADICIONAIS
        // ========================================

        // Configurar precisão de decimais para Peças
        modelBuilder.Entity<Peca>()
            .Property(p => p.Preco)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Peca>()
            .Property(p => p.Peso)
            .HasPrecision(10, 2);

        // Configurar precisão de decimais para Encomendas
        modelBuilder.Entity<Encomenda>()
            .Property(e => e.ValorTotal)
            .HasPrecision(18, 2);

        // Configurar precisão de decimais para ItemEncomenda
        modelBuilder.Entity<ItemEncomenda>()
            .Property(i => i.PrecoUnitario)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ItemEncomenda>()
            .Property(i => i.Desconto)
            .HasPrecision(5, 2);

        // ========================================
        // ÍNDICES PARA MELHOR PERFORMANCE
        // ========================================

        // Índice único na Referência da Peça
        modelBuilder.Entity<Peca>()
            .HasIndex(p => p.Referencia)
            .IsUnique();

        // Índice único no Email do Cliente
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.Email)
            .IsUnique();

        // Índice único no NIF do Cliente
        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.NIF)
            .IsUnique();

        // Índice na Data da Encomenda para queries mais rápidas
        modelBuilder.Entity<Encomenda>()
            .HasIndex(e => e.DataEncomenda);

        // Índice no Estado da Encomenda para filtros
        modelBuilder.Entity<Encomenda>()
            .HasIndex(e => e.Estado);
    }
}