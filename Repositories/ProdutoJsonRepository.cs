using System.Text.Encodings.Web;
using ListaExercicio01App.Entities;
using System.Text.Json;

namespace ListaExercicio01App.Repositories
{
    public class ProdutoJsonRepository
    {
        public void ExportarDados(Produto produto)
        {
            var diretorio = "c:\\temp";
            var caminho = Path.Combine(diretorio, $"produto_{produto.DataCompra:yyyy-MM-dd}-{produto.IdProduto}.json");

            Directory.CreateDirectory(diretorio);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var json = JsonSerializer.Serialize(produto, options);

            File.WriteAllText(caminho, json);


        }
    }
}
