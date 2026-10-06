namespace ListaExercicio01App.Services
{
    public class MenuService
    {
        public void ExecutarMenuPrincipal()
        {
            Console.WriteLine("\nMenu Principal\n");
            Console.WriteLine("(1) Categorias");
            Console.WriteLine("(2) Fornecedores");
            Console.WriteLine("(3) Produtos");
            Console.WriteLine("(0) Sair");

            Console.Write("Informe a opção desejada:");
            var opcao = int.Parse(Console.ReadLine() ?? string.Empty);

            switch (opcao)
            {
                case 1: Console.Clear();
                    new CategoriaService().ExecutarMenu();
                    break;
                
                case 2: Console.Clear();
                    new FornecedorService().ExecutarMenu();
                    break;
                
                case 3: Console.Clear();
                    new ProdutoService().ExecutarMenu();
                    break;
                
                case 0:
                    Console.WriteLine("\nFim do Programa!");
                    break;
                
                default:
                    Console.WriteLine("\nOpção Inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.Clear();
                ExecutarMenuPrincipal();
            }
        }
    }
}
