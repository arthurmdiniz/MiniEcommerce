namespace MiniEcommerce.Console.Dominio;

public class Produto
{
    public string Nome { get; }
    public decimal Preco { get; }
    public int Estoque { get; private set; }

    public Produto(string nome, decimal preco, int estoque)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));

        if (preco < 0)
            throw new ArgumentException("O preço não pode ser negativo.", nameof(preco));

        if (estoque < 0)
            throw new ArgumentException("O estoque não pode ser negativo.", nameof(estoque));

        Nome = nome;
        Preco = preco;
        Estoque = estoque;
    }

    public void BaixarEstoque(int quantidade)
    {
        if (quantidade > Estoque)
            throw new InvalidOperationException($"Estoque insuficiente para '{Nome}'. Disponível: {Estoque}.");

        Estoque -= quantidade;
    }
}
