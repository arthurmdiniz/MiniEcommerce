namespace MiniEcommerce.Console.Dominio;

public class Pedido
{
    private readonly List<ItemCarrinho> _itens;

    public IReadOnlyList<ItemCarrinho> Itens => _itens;
    public decimal Total => _itens.Sum(i => i.Subtotal());

    private Pedido(List<ItemCarrinho> itens)
    {
        _itens = itens;
    }

    public static Pedido Finalizar(CarrinhoDeCompras carrinho)
    {
        if (carrinho.Itens.Count == 0)
            throw new InvalidOperationException("Não é possível finalizar um carrinho vazio.");

        foreach (var item in carrinho.Itens)
        {
            if (item.Quantidade > item.Produto.Estoque)
                throw new InvalidOperationException($"Estoque insuficiente para '{item.Produto.Nome}'. Disponível: {item.Produto.Estoque}.");
        }

        var itens = new List<ItemCarrinho>();
        foreach (var item in carrinho.Itens)
        {
            item.Produto.BaixarEstoque(item.Quantidade);
            itens.Add(new ItemCarrinho(item.Produto, item.Quantidade));
        }

        var pedido = new Pedido(itens);
        carrinho.Limpar();
        return pedido;
    }
}
