using MiniEcommerce.Console.Dominio;

namespace MiniEcommerce.Tests;

public class CarrinhoDeComprasTests
{
    [Fact]
    public void Adicionar_ComQuantidadeDentroDoEstoque_DeveAdicionarItem()
    {
        var carrinho = new CarrinhoDeCompras();
        var produto = new Produto("Teclado", 100m, 5);

        carrinho.Adicionar(produto, 2);

        Assert.Single(carrinho.Itens);
        Assert.Equal(2, carrinho.Itens[0].Quantidade);
    }

    [Fact]
    public void Adicionar_ComQuantidadeMaiorQueEstoque_DeveLancarExcecao()
    {
        var carrinho = new CarrinhoDeCompras();
        var produto = new Produto("Teclado", 100m, 5);

        Assert.Throws<InvalidOperationException>(() => carrinho.Adicionar(produto, 6));
    }

    [Fact]
    public void Adicionar_QuantidadeAcumuladaMaiorQueEstoque_DeveLancarExcecao()
    {
        var carrinho = new CarrinhoDeCompras();
        var produto = new Produto("Teclado", 100m, 5);
        carrinho.Adicionar(produto, 3);

        Assert.Throws<InvalidOperationException>(() => carrinho.Adicionar(produto, 3));
    }

    [Fact]
    public void Adicionar_QuantidadeZeroOuNegativa_DeveLancarExcecao()
    {
        var carrinho = new CarrinhoDeCompras();
        var produto = new Produto("Teclado", 100m, 5);

        Assert.Throws<ArgumentException>(() => carrinho.Adicionar(produto, 0));
        Assert.Throws<ArgumentException>(() => carrinho.Adicionar(produto, -1));
    }

    [Fact]
    public void Adicionar_MesmoProdutoDuasVezes_DeveSomarQuantidade()
    {
        var carrinho = new CarrinhoDeCompras();
        var produto = new Produto("Teclado", 100m, 5);

        carrinho.Adicionar(produto, 2);
        carrinho.Adicionar(produto, 1);

        Assert.Single(carrinho.Itens);
        Assert.Equal(3, carrinho.Itens[0].Quantidade);
    }

    [Fact]
    public void Subtotal_DeveSomarPrecoVezesQuantidadeDeCadaItem()
    {
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(new Produto("Teclado", 100m, 5), 2);
        carrinho.Adicionar(new Produto("Mouse", 50m, 5), 3);

        Assert.Equal(350m, carrinho.Subtotal());
    }

    [Fact]
    public void Remover_ProdutoNoCarrinho_DeveRemoverItem()
    {
        var carrinho = new CarrinhoDeCompras();
        var produto = new Produto("Teclado", 100m, 5);
        carrinho.Adicionar(produto, 2);

        carrinho.Remover(produto);

        Assert.Empty(carrinho.Itens);
    }

    [Fact]
    public void Remover_ProdutoQueNaoEstaNoCarrinho_DeveLancarExcecao()
    {
        var carrinho = new CarrinhoDeCompras();

        Assert.Throws<InvalidOperationException>(() => carrinho.Remover(new Produto("Mouse", 50m, 5)));
    }
}
