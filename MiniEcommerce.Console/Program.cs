using MiniEcommerce.Console.Dominio;

var catalogo = new Catalogo();
var carrinho = new CarrinhoDeCompras();
var pedidos = new List<Pedido>();

while (true)
{
    System.Console.WriteLine();
    System.Console.WriteLine("=== MiniEcommerce ===");
    System.Console.WriteLine("1 - Cadastrar produto");
    System.Console.WriteLine("2 - Listar catálogo");
    System.Console.WriteLine("3 - Adicionar produto ao carrinho");
    System.Console.WriteLine("4 - Remover produto do carrinho");
    System.Console.WriteLine("5 - Ver carrinho");
    System.Console.WriteLine("6 - Finalizar pedido");
    System.Console.WriteLine("0 - Sair");
    System.Console.Write("Escolha uma opção: ");

    var opcao = System.Console.ReadLine();
    System.Console.WriteLine();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarProduto();
                break;
            case "2":
                ListarCatalogo();
                break;
            case "3":
                AdicionarAoCarrinho();
                break;
            case "4":
                RemoverDoCarrinho();
                break;
            case "5":
                VerCarrinho();
                break;
            case "6":
                FinalizarPedido();
                break;
            case "0":
                return;
            default:
                System.Console.WriteLine("Opção inválida.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        System.Console.WriteLine($"Erro: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        System.Console.WriteLine($"Erro: {ex.Message}");
    }
}

void CadastrarProduto()
{
    System.Console.Write("Nome: ");
    var nome = System.Console.ReadLine() ?? string.Empty;
    var preco = LerDecimal("Preço: ");
    var estoque = LerInteiro("Quantidade em estoque: ");

    catalogo.Cadastrar(new Produto(nome, preco, estoque));
    System.Console.WriteLine("Produto cadastrado com sucesso.");
}

void ListarCatalogo()
{
    if (catalogo.Produtos.Count == 0)
    {
        System.Console.WriteLine("Nenhum produto cadastrado.");
        return;
    }

    foreach (var produto in catalogo.Produtos)
        System.Console.WriteLine($"{produto.Nome} | Preço: {produto.Preco:C} | Estoque: {produto.Estoque}");
}

void AdicionarAoCarrinho()
{
    var produto = BuscarProduto();
    var quantidade = LerInteiro("Quantidade desejada: ");

    carrinho.Adicionar(produto, quantidade);
    System.Console.WriteLine("Produto adicionado ao carrinho.");
}

void RemoverDoCarrinho()
{
    var produto = BuscarProduto();

    carrinho.Remover(produto);
    System.Console.WriteLine("Produto removido do carrinho.");
}

void VerCarrinho()
{
    if (carrinho.Itens.Count == 0)
    {
        System.Console.WriteLine("O carrinho está vazio.");
        return;
    }

    foreach (var item in carrinho.Itens)
        System.Console.WriteLine($"{item.Produto.Nome} | Quantidade: {item.Quantidade} | Subtotal: {item.Subtotal():C}");

    System.Console.WriteLine($"Total: {carrinho.Subtotal():C}");
}

void FinalizarPedido()
{
    var pedido = Pedido.Finalizar(carrinho);
    pedidos.Add(pedido);

    System.Console.WriteLine($"Pedido #{pedidos.Count} finalizado com sucesso.");
    foreach (var item in pedido.Itens)
        System.Console.WriteLine($"{item.Produto.Nome} | Quantidade: {item.Quantidade} | Subtotal: {item.Subtotal():C}");
    System.Console.WriteLine($"Total do pedido: {pedido.Total:C}");
}

Produto BuscarProduto()
{
    System.Console.Write("Nome do produto: ");
    var nome = System.Console.ReadLine() ?? string.Empty;

    return catalogo.BuscarPorNome(nome)
        ?? throw new InvalidOperationException($"Produto '{nome}' não encontrado.");
}

int LerInteiro(string mensagem)
{
    System.Console.Write(mensagem);
    if (!int.TryParse(System.Console.ReadLine(), out var valor))
        throw new ArgumentException("Informe um número inteiro válido.");
    return valor;
}

decimal LerDecimal(string mensagem)
{
    System.Console.Write(mensagem);
    if (!decimal.TryParse(System.Console.ReadLine(), out var valor))
        throw new ArgumentException("Informe um valor numérico válido.");
    return valor;
}
