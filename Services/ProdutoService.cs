using ListaExercicio01App.Entities;
using ListaExercicio01App.Repositories;
using ListaExercicio01App.Validators;

namespace ListaExercicio01App.Services
{
    public class ProdutoService
    {
        public void ExecutarMenu()
        {
            Console.WriteLine("\nMenu de Produtos\n");

            Console.WriteLine("(1) Cadastrar");
            Console.WriteLine("(2) Atualizar");
            Console.WriteLine("(3) Excluir");
            Console.WriteLine("(4) Consultar");
            Console.WriteLine("(5) Exportar");
            Console.WriteLine("(6) Voltar");

            Console.Write("Informe a opção desejada: ");
            int.TryParse(Console.ReadLine(), out var opcao);

            switch (opcao)
            {
                case 1: Cadastrar();
                    break;

                case 2: Atualizar();
                    break;

                case 3: Excluir();
                    break;

                case 4: Consultar();
                    break;

                case 5: Exportar();
                    break;
                
                case 6:
                    break;

                default:
                    Console.WriteLine("\nOpção Inválida!");
                    break;
            }

            Console.WriteLine("\nPressione Uma Tecla");
            Console.ReadKey();

            if (opcao != 6)
            {
                Console.Clear();
                ExecutarMenu();
            }
        }

        private void Cadastrar()
        {
            var fornecedores = new FornecedorRepository().ObterTodosFornecedores();
            var categorias = new CategoriaRepository().ObterTodasCategorias();

            Console.WriteLine("\nCadastrar Produto");

            if (fornecedores.Count == 0 || categorias.Count == 0)
            {
                Console.WriteLine("Cadastre pelo menos um fornecedor e uma categoria!");
                
                return;
            }

            var produto = new Produto();

            Console.Write("Digite o Nome: ");
            produto.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o Preço: ");
            double.TryParse(Console.ReadLine(), out var preco);
            produto.Preco = preco;
            
            Console.Write("Digite a Quantidade: ");
            int.TryParse(Console.ReadLine(), out var quantidade);
            produto.Quantidade = quantidade;

            Console.Write("Digite a Data da Compra (dd/mm/aaaa): ");
            if (DateTime.TryParse(Console.ReadLine(), out var dataCompra))
                produto.DataCompra = dataCompra;

            // Escolher Fornecedor
            for (int i = 0; i < fornecedores.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {fornecedores[i].Nome}");
            }

            Console.Write("Informe o Número do Fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out var numeroFornecedor)
                || numeroFornecedor < 1 || numeroFornecedor > fornecedores.Count)
            {
                Console.WriteLine("\nFornecedor Inválido.");
                return;
            }

            produto.Fornecedor = fornecedores[numeroFornecedor - 1];

            //Escolher a categoria
            Console.WriteLine("\nCategorias");
            for (int i = 0; i < categorias.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {categorias[i].Descricao}");
            }

            Console.Write("Informe o Número da Categoria: ");
            if (!int.TryParse(Console.ReadLine(), out var numeroCategoria)
                || numeroCategoria < 1 || numeroCategoria > categorias.Count)
            {
                Console.WriteLine("\nCategoria Inválida.");
                return;
            }

            produto.Categoria = categorias[numeroCategoria - 1];

            if (ObjetoValidator.ValidarObjeto(produto))
            {
                var produtoRepository = new ProdutoRepository();
                produtoRepository.InserirProduto(produto);

                new ProdutoJsonRepository().ExportarDados(produto);
                new ProdutoXmlRepository().ExportarDados(produto);

                Console.WriteLine("\nProduto Cadastrado e Exportado Com Sucesso!");
            }

        }

        private void Atualizar()
        {
            Console.WriteLine("\nAtualizar Produto\n");

            var produtoRepository = new ProdutoRepository();

            var produtos = produtoRepository.ObterTodosProdutos();
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum Produto Cadastrado");
                return;
            }


            for (int i = 0; i < produtos.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {produtos[i].Nome}");
            }

            Console.Write("Informe o Número do Produto: ");
            if (!int.TryParse(Console.ReadLine(), out var numero)
                || numero < 1 || numero > produtos.Count)
            {
                Console.WriteLine("\nOpção Inválida.");
                return;
            }

            var produto = produtos[numero - 1];

            Console.WriteLine("\nDados do Produto");
            Console.WriteLine("\tNome.......: " + produto.Nome);
            Console.WriteLine($"\tPreço......: {produto.Preco:C}");
            Console.WriteLine("\tQuantidade.: " + produto.Quantidade);
            Console.WriteLine($"\tData.......: {produto.DataCompra:dd/MM/yyyy}");
            Console.WriteLine("\tFornecedor.: " + produto.Fornecedor?.Nome);
            Console.WriteLine("\tCategoria..: " + produto.Categoria?.Descricao);

            Console.WriteLine("\nInforme os Novos Dados\n");

            Console.Write("Digite o Nome: ");
            produto.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o Preço: ");
            double.TryParse(Console.ReadLine(), out var preco);
            produto.Preco = preco;

            Console.Write("Digite a Quantidade: ");
            int.TryParse(Console.ReadLine(), out var quantidade);
            produto.Quantidade = quantidade;

            Console.Write("Digite a Data da Compra (dd/mm/aaaa): ");
            if (DateTime.TryParse(Console.ReadLine(), out var dataCompra))
                produto.DataCompra = dataCompra;
            else
                produto.DataCompra = null;

            if (ObjetoValidator.ValidarObjeto(produto))
            {
                produtoRepository.AtualizarProduto(produto);

                Console.WriteLine("\nProduto Atualizado Com Sucesso!");
            }
        }

        private void Excluir()
        {
            Console.WriteLine("\nExcluir Produto\n");

            var produtoRepository = new ProdutoRepository();

            var produtos = produtoRepository.ObterTodosProdutos();
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum Produto Cadastrado");
                return;
            }

            for (int i = 0; i < produtos.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {produtos[i].Nome}");
            }

            Console.Write("Informe o Número do Produto: ");
            if (!int.TryParse(Console.ReadLine(), out var numero)
                || numero < 1 || numero > produtos.Count)
            {
                Console.WriteLine("\nOpção Inválida.");
                return;
            }

            var produto = produtos[numero - 1];

            Console.WriteLine("\nDados do Produto");
            Console.WriteLine("\tNome.......: " + produto.Nome);
            Console.WriteLine($"\tPreço......: {produto.Preco:C}");
            Console.WriteLine("\tFornecedor.: " + produto.Fornecedor?.Nome);
            Console.WriteLine("\tCategoria..: " + produto.Categoria?.Descricao);

            Console.Write("\nDeseja Realmente Excluir? (S/N): ");
            var confirmacao = Console.ReadLine() ?? string.Empty;

            if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                produtoRepository.ExcluirProduto(produto.IdProduto);

                Console.WriteLine("\nProduto Excluído Com Sucesso!");
            }
        }

        private void Exportar()
        {
            Console.WriteLine("\nExportar Produtos\n");

            var produtoRepository = new ProdutoRepository();
            var produtos = produtoRepository.ObterTodosProdutos();

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum Produto Cadastrado Para Exportar");
                return;
            }

            var produtoJsonRepository = new ProdutoJsonRepository();
            var produtoXmlRepository = new ProdutoXmlRepository();

            foreach (var produto in produtos)
            {
                produtoJsonRepository.ExportarDados(produto);
                produtoXmlRepository.ExportarDados(produto);
            }

            Console.WriteLine($"{produtos.Count} Produto(s) Exportado(s) Para c:\\temp");
        }
    }
    
}
