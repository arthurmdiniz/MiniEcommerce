using MiniEcommerce.Console.Dominio;

namespace MiniEcommerce.Tests;

public class PedidoTests
{
    [Fact]
    public void Finalizar_DeveDarBaixaNoEstoqueDosProdutos()
    {
        var teclado = new Produto("Teclado", 100m, 5);
        var mouse = new Produto("Mouse", 50m, 10);
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(teclado, 2);
        carrinho.Adicionar(mouse, 3);

        Pedido.Finalizar(carrinho);

        Assert.Equal(3, teclado.Estoque);
        Assert.Equal(7, mouse.Estoque);
    }

    [Fact]
    public void Finalizar_DeveCalcularTotalDoPedido()
    {
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(new Produto("Teclado", 100m, 5), 2);
        carrinho.Adicionar(new Produto("Mouse", 50m, 5), 3);

        var pedido = Pedido.Finalizar(carrinho);

        Assert.Equal(350m, pedido.Total);
        Assert.Equal(2, pedido.Itens.Count);
    }

    [Fact]
    public void Finalizar_DeveLimparCarrinho()
    {
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(new Produto("Teclado", 100m, 5), 2);

        Pedido.Finalizar(carrinho);

        Assert.Empty(carrinho.Itens);
    }

    [Fact]
    public void Finalizar_CarrinhoVazio_DeveLancarExcecao()
    {
        var carrinho = new CarrinhoDeCompras();

        Assert.Throws<InvalidOperationException>(() => Pedido.Finalizar(carrinho));
    }

    [Fact]
    public void Finalizar_ComEstoqueInsuficienteNoCheckout_NaoDeveAlterarEstoqueNemCarrinho()
    {
        var teclado = new Produto("Teclado", 100m, 5);
        var mouse = new Produto("Mouse", 50m, 5);
        var carrinho = new CarrinhoDeCompras();
        carrinho.Adicionar(teclado, 2);
        carrinho.Adicionar(mouse, 4);

        var outroCarrinho = new CarrinhoDeCompras();
        outroCarrinho.Adicionar(mouse, 3);
        Pedido.Finalizar(outroCarrinho);

        Assert.Throws<InvalidOperationException>(() => Pedido.Finalizar(carrinho));
        Assert.Equal(5, teclado.Estoque);
        Assert.Equal(2, mouse.Estoque);
        Assert.Equal(2, carrinho.Itens.Count);
    }
}
