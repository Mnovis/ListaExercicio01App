using ListaExercicio01App.Entities;
using System.Xml;
using System.Xml.Serialization;

namespace ListaExercicio01App.Repositories
{
    public class ProdutoXmlRepository
    {
        public void ExportarDados(Produto produto)
        {
            var xmlSerializer = new XmlSerializer(typeof(Produto));

            var diretorio = "c:\\temp";
            var caminho = Path.Combine(diretorio, $"produto_{produto.DataCompra:yyyy-MM-dd}-{produto.IdProduto}.xml");

            Directory.CreateDirectory(diretorio);

            using (var writer = XmlWriter.Create(caminho))
            {
                xmlSerializer.Serialize(writer, produto);
            }
        }
    }
}
