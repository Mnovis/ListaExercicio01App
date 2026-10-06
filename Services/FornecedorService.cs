using ListaExercicio01App.Entities;
using ListaExercicio01App.Repositories;
using ListaExercicio01App.Validators;
using Microsoft.Data.SqlClient;

namespace ListaExercicio01App.Services
{
    public class FornecedorService
    {
        public void ExecutarMenu()
        {
            Console.WriteLine("\nMenu de Fornecedor\n");

            Console.WriteLine("(1) Cadastrar");
            Console.WriteLine("(2) Atualizar");
            Console.WriteLine("(3) Excluir");
            Console.WriteLine("(4) Consultar");
            Console.WriteLine("(5) Voltar");

            Console.Write("Informe a opção desejada: ");
            int.TryParse(Console.ReadLine(), out var opcao);

            switch (opcao)
            {
                case 1:
                    Cadastrar();
                    break;

                case 2:
                    Atualizar();
                    break;

                case 3:
                    Excluir();
                    break;

                case 4:
                    Consultar();
                    break;

                case 5:
                    break;

                default:
                    Console.WriteLine("\nOpção Inválida!");
                    break;
            }

            Console.WriteLine("\nPressione Uma Tecla");
            Console.ReadKey();

            if (opcao != 5)
            {
                Console.Clear();
                ExecutarMenu();
            }
        }

        private void Cadastrar()
        {
            Console.WriteLine("\nCadastrar Fornecedor\n");

            var fornecedor = new Fornecedor();

            Console.Write("Digite o Nome: ");
            fornecedor.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o CNPJ: ");
            fornecedor.Cnpj = Console.ReadLine() ?? string.Empty;

            if (ObjetoValidator.ValidarObjeto(fornecedor))
            {
                var fornecedorRepository = new FornecedorRepository();
                if (fornecedorRepository.VerificarCnpj(fornecedor.Cnpj))
                {
                    Console.WriteLine("\nEste CNPJ já está cadastrado para outro fornecedor.");
                    return;
                }

                fornecedorRepository.InserirFornecedor(fornecedor);

                Console.WriteLine("\nFornecedor Cadastrado Com Sucesso!");
            }
        }

        private void Atualizar()
        {
            Console.WriteLine("\nAtualizar Fornecedor\n");

            var fornecedorRepository = new FornecedorRepository();

            var fornecedor = EscolherFornecedor(fornecedorRepository.ObterTodosFornecedores());
            if (fornecedor == null)
                return;

            Console.WriteLine("\nDados do Fornecedor");
            Console.WriteLine("\tNome: " + fornecedor.Nome);
            Console.WriteLine("\tCNPJ: " + fornecedor.Cnpj);

            Console.WriteLine("\nInforme os Novos Dados\n");

            Console.Write("Digite o Nome: ");
            fornecedor.Nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o CNPJ: ");
            fornecedor.Cnpj = Console.ReadLine() ?? string.Empty;

            if (ObjetoValidator.ValidarObjeto(fornecedor))
            {
                if (fornecedorRepository.VerificarCnpj(fornecedor.Cnpj, fornecedor.IdFornecedor))
                {
                    Console.WriteLine("\nEste CNPJ já está cadastrado para outro fornecedor.");
                    return;
                }

                fornecedorRepository.AtualizarFornecedor(fornecedor);

                Console.WriteLine("\nFornecedor Atualizado Com Sucesso!");
            }
        }

        private void Excluir()
        {
            Console.WriteLine("\nExcluir Fornecedor\n");

            var fornecedorRepository = new FornecedorRepository();

            var fornecedor = EscolherFornecedor(fornecedorRepository.ObterTodosFornecedores());
            if (fornecedor == null)
                return;

            Console.WriteLine("\nDados do Fornecedor");
            Console.WriteLine("\tNome: " + fornecedor.Nome);
            Console.WriteLine("\tCNPJ: " + fornecedor.Cnpj);

            Console.Write("\nDeseja Realmente Excluir? (S/N): ");
            var confirmacao = Console.ReadLine() ?? string.Empty;

            if (confirmacao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    fornecedorRepository.ExcluirFornecedor(fornecedor.IdFornecedor);

                    Console.WriteLine("\nFornecedor Excluído Com Sucesso!");
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    Console.WriteLine("\nNão é possível excluir este fornecedor, pois ele possui produtos cadastrados.");
                }
            }
        }

        private void Consultar()
        {
            Console.WriteLine("\nConsulta de Fornecedores\n");

            var fornecedorRepository = new FornecedorRepository();
            var fornecedores = fornecedorRepository.ObterTodosFornecedores();

            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum Fornecedor Cadastrado");
                return;
            }

            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine($"Nome: {fornecedor.Nome}, CNPJ: {fornecedor.Cnpj}");
            }
        }

        /// <summary>
        /// Mostra a lista numerada e devolve o fornecedor escolhido,
        /// ou null se a lista estiver vazia ou a opção for inválida.
        /// Usado pelo Atualizar e pelo Excluir, para não repetir código.
        /// </summary>
        private Fornecedor? EscolherFornecedor(List<Fornecedor> fornecedores)
        {
            if (fornecedores.Count == 0)
            {
                Console.WriteLine("Nenhum Fornecedor Cadastrado");
                return null;
            }

            for (int i = 0; i < fornecedores.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {fornecedores[i].Nome}  CNPJ: {fornecedores[i].Cnpj}");
            }

            Console.Write("\nInforme o Número do Fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out var numero)
                || numero < 1 || numero > fornecedores.Count)
            {
                Console.WriteLine("\nOpção Inválida.");
                return null;
            }

            return fornecedores[numero - 1];
        }
    }
}