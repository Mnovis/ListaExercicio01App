using ListaExercicio01App.Entities;
using ListaExercicio01App.Repositories;
using ListaExercicio01App.Validators;

namespace ListaExercicio01App.Services
{
    public class CategoriaService
    {
        public void ExecutarMenu()
        {
            Console.WriteLine("\nMenu de Categoria");

            Console.WriteLine("(1) Cadastrar");
            Console.WriteLine("(2) Atualizar");
            Console.WriteLine("(3) Excluir");
            Console.WriteLine("(4) Consultar");
            Console.WriteLine("(5) Voltar");

            Console.Write("Informe a opção desejada:");
            int.TryParse(Console.ReadLine(), out var opcao);

            switch (opcao)
            {
                case 1: Cadastrar();
                    break;

                case 2: Atualizar();
                    break;

                case 3: Excluir();
                    break;

                case 4: ObterTodas();
                    break;
                
                case 5: 
                    break;
                
                default:
                    Console.WriteLine("\nOpção inválida!");
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
            var categoria = new Categoria();

            Console.WriteLine("\nCadastrar Categoria");

            Console.Write("Digite a Descrição: ");
            categoria.Descricao = Console.ReadLine() ?? string.Empty;

            if (ObjetoValidator.ValidarObjeto(categoria))
            {
                var categoriaRepository = new CategoriaRepository();
                categoriaRepository.InserirCategoria(categoria);

                Console.WriteLine("\nCategoria Cadastrada Com Sucesso!");
            }
        }

        private void Atualizar()
        {
                Console.WriteLine("\nAtualizar Categoria");

                var categoriaRepository = new CategoriaRepository();

                var categorias = categoriaRepository.ObterTodasCategorias();
                if (categorias.Count == 0)
                {
                    Console.WriteLine("Nenhuma Categoria Cadastrada");
                    return;
                }

                for (int i = 0; i < categorias.Count; i++)
                {
                    Console.WriteLine($"({i + 1}) {categorias[i].Descricao}");
                }

                Console.Write("Informe o Número da Categoria: ");
                if (!int.TryParse(Console.ReadLine(), out var numero)
                    || numero < 1 || numero > categorias.Count)
                {
                    Console.WriteLine("\nOpção Inválida.");
                    return;
                }

                var categoria = categorias[numero - 1];

                Console.WriteLine("\nDados da Categoria");
                Console.WriteLine("\tDescrição: " + categoria.Descricao);

                Console.Write("\nDigite a Nova Descrição: ");
                categoria.Descricao = Console.ReadLine() ?? string.Empty;

                if (ObjetoValidator.ValidarObjeto(categoria))
                {
                    categoriaRepository.AtualizarCategoria(categoria);

                    Console.WriteLine("\nCategoria Atualizada Com Sucesso!");
                }
        }

        private void Excluir()
        {
            Console.WriteLine("\nExcluir Categoria");

            var categoriaRepository = new CategoriaRepository();

            var categorias = categoriaRepository.ObterTodasCategorias();
            if (categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma Categoria Cadastrada");
                return;
            }

            for (int i = 0; i < categorias.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {categorias[i].Descricao}");
            }

            Console.Write("Informe o Número da Categoria: ");
            if (!int.TryParse(Console.ReadLine(), out var numero)
                || numero < 1 || numero > categorias.Count)
            {
                Console.WriteLine("\nOpção Inválida.");
                return;
            }

            var categoria = categorias[numero - 1];

            Console.WriteLine("\nDados da Categoria");
            Console.WriteLine("\tDescrição: " + categoria.Descricao);

            Console.Write("Deseja Realmente Excluir? (S/N): ");
            var opcao = Console.ReadLine() ?? string.Empty;

            if (opcao.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    categoriaRepository.ExcluirCategoria(categoria.IdCategoria);

                    Console.WriteLine("\nCategoria Excluída Com Sucesso!");
                }
                catch
                {
                    Console.WriteLine("Não é possível Excluir Essa Categoria, pois ela possui produtos Cadastrados");
                }
            }
        }

        private void ObterTodas()
        {
            Console.WriteLine("\nConsulta de Categorias\n");

            var categoriaRepository = new CategoriaRepository();
            var categorias = categoriaRepository.ObterTodasCategorias();

            if (categorias.Count == 0)
            {
                Console.WriteLine("Nenhuma Categoria Cadastrada");
                return;
            }

            foreach (var categoria in categorias)
            {
                Console.WriteLine($"Descrição: {categoria.Descricao}");
            }
        }
    }
}
