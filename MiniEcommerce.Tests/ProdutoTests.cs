using MiniEcommerce.Console.Dominio;

namespace MiniEcommerce.Tests;

public class ProdutoTests
{
    [Fact]
    public void Criar_ComDadosValidos_DeveCriarProduto()
    {
        var produto = new Produto("Teclado", 150.00m, 10);

        Assert.Equal("Teclado", produto.Nome);
        Assert.Equal(150.00m, produto.Preco);
        Assert.Equal(10, produto.Estoque);
    }

    [Fact]
    public void Criar_ComPrecoNegativo_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Produto("Teclado", -1m, 10));
    }

    [Fact]
    public void Criar_ComEstoqueNegativo_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Produto("Teclado", 150m, -1));
    }

    [Fact]
    public void Criar_ComNomeVazio_DeveLancarExcecao()
    {
        Assert.Throws<ArgumentException>(() => new Produto("   ", 150m, 10));
    }

    [Fact]
    public void Criar_ComPrecoZeroEEstoqueZero_DeveSerPermitido()
    {
        var produto = new Produto("Brinde", 0m, 0);

        Assert.Equal(0m, produto.Preco);
        Assert.Equal(0, produto.Estoque);
    }
}
