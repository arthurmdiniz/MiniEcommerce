# MiniEcommerce

Aplicação de console em C# (.NET 10) que simula uma pequena loja virtual: cadastro de produtos, catálogo, carrinho de compras e finalização de pedidos com baixa automática no estoque.

## Estrutura do projeto

```
MiniEcommerce.slnx
├── MiniEcommerce.Console/      # Aplicação de console
│   ├── Program.cs              # Menu interativo
│   └── Dominio/
│       ├── Produto.cs          # Entidade com validação de preço e estoque
│       ├── Catalogo.cs         # Cadastro e busca de produtos
│       ├── CarrinhoDeCompras.cs# Adição, remoção e subtotal, com verificação de estoque
│       ├── ItemCarrinho.cs     # Produto e quantidade dentro do carrinho
│       └── Pedido.cs           # Checkout: valida estoque, dá baixa e gera o total
└── MiniEcommerce.Tests/        # Testes unitários (xUnit)
```

### Regras de negócio

- Produtos não podem ter preço negativo, estoque negativo ou nome vazio.
- O carrinho só aceita quantidades positivas e não pode exceder o estoque disponível.
- No checkout, todo o estoque é verificado antes da baixa. Se algum item não tiver estoque, nenhum estoque é alterado.
- Após a finalização, o carrinho é esvaziado.

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

## Como executar

```bash
dotnet run --project MiniEcommerce.Console/MiniEcommerce.Console.csproj
```

## Como testar

```bash
dotnet test
```

## Desenvolvimento

O projeto foi desenvolvido com TDD básico: os testes de cada funcionalidade são escritos antes da implementação. Os commits seguem o padrão *Conventional Commits*.
