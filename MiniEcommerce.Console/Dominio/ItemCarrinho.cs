namespace MiniEcommerce.Console.Dominio;

public class ItemCarrinho
{
    public Produto Produto { get; }
    public int Quantidade { get; internal set; }

    public ItemCarrinho(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }

    public decimal Subtotal() => Produto.Preco * Quantidade;
}
