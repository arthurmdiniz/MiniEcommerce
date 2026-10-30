namespace MiniEcommerce.Console.Dominio;

public class CarrinhoDeCompras
{
    private readonly List<ItemCarrinho> _itens = new();

    public IReadOnlyList<ItemCarrinho> Itens => _itens;

    public void Adicionar(Produto produto, int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

        var item = _itens.FirstOrDefault(i => i.Produto == produto);
        var quantidadeTotal = (item?.Quantidade ?? 0) + quantidade;

        if (quantidadeTotal > produto.Estoque)
            throw new InvalidOperationException($"Estoque insuficiente para '{produto.Nome}'. Disponível: {produto.Estoque}.");

        if (item is null)
            _itens.Add(new ItemCarrinho(produto, quantidade));
        else
            item.Quantidade = quantidadeTotal;
    }

    public void Remover(Produto produto)
    {
        var item = _itens.FirstOrDefault(i => i.Produto == produto)
            ?? throw new InvalidOperationException($"O produto '{produto.Nome}' não está no carrinho.");

        _itens.Remove(item);
    }

    public decimal Subtotal() => _itens.Sum(i => i.Subtotal());

    public void Limpar() => _itens.Clear();
}
