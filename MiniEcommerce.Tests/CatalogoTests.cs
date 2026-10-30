using MiniEcommerce.Console.Dominio;

namespace MiniEcommerce.Tests;

public class CatalogoTests
{
    [Fact]
    public void Cadastrar_DeveAdicionarProdutoAoCatalogo()
    {
        var catalogo = new Catalogo();

        catalogo.Cadastrar(new Produto("Teclado", 100m, 5));

        Assert.Single(catalogo.Produtos);
    }

    [Fact]
    public void Cadastrar_NomeJaExistente_DeveLancarExcecao()
    {
        var catalogo = new Catalogo();
        catalogo.Cadastrar(new Produto("Teclado", 100m, 5));

        Assert.Throws<InvalidOperationException>(() => catalogo.Cadastrar(new Produto("teclado", 80m, 2)));
    }

    [Fact]
    public void BuscarPorNome_DeveIgnorarMaiusculasEMinusculas()
    {
        var catalogo = new Catalogo();
        var produto = new Produto("Teclado", 100m, 5);
        catalogo.Cadastrar(produto);

        Assert.Same(produto, catalogo.BuscarPorNome("TECLADO"));
    }

    [Fact]
    public void BuscarPorNome_ProdutoInexistente_DeveRetornarNulo()
    {
        var catalogo = new Catalogo();

        Assert.Null(catalogo.BuscarPorNome("Mouse"));
    }
}
