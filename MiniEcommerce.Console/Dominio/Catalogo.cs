namespace MiniEcommerce.Console.Dominio;

public class Catalogo
{
    private readonly List<Produto> _produtos = new();

    public IReadOnlyList<Produto> Produtos => _produtos;

    public void Cadastrar(Produto produto)
    {
        if (BuscarPorNome(produto.Nome) is not null)
            throw new InvalidOperationException($"Já existe um produto cadastrado com o nome '{produto.Nome}'.");

        _produtos.Add(produto);
    }

    public Produto? BuscarPorNome(string nome) =>
        _produtos.FirstOrDefault(p => string.Equals(p.Nome, nome, StringComparison.OrdinalIgnoreCase));
}
