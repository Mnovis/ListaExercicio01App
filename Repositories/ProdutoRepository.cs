using Dapper;
using ListaExercicio01App.Entities;
using Microsoft.Data.SqlClient;

namespace ListaExercicio01App.Repositories
{
    public class ProdutoRepository
    {
        #region Atributos Privados

        private readonly string _connectionString = "Server=localhost,1435; Database=master; User Id=sa; Password=Coti@2026; TrustServerCertificate=True";

        #endregion

        #region Métodos

        public void InserirProduto(Produto produto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                                   INSERT INTO PRODUTOS(IDPRODUTO, NOME, PRECO, QUANTIDADE, DATACOMPRA, IDFORNECEDOR, IDCATEGORIA)
                                   VALUES(@IdProduto, @Nome, @Preco, @Quantidade, @DataCompra, @IdFornecedor, @IdCategoria)
                                   """, new
                {
                    @IdProduto = produto.IdProduto,
                    @Nome = produto.Nome,
                    @Preco = produto.Preco,
                    @Quantidade = produto.Quantidade,
                    @DataCompra = produto.DataCompra,
                    @IdFornecedor = produto.Fornecedor!.IdFornecedor,
                    @IdCategoria = produto.Categoria!.IdCategoria

                });
            }
        }

        public void AtualizarProduto(Produto produto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                                   UPDATE PRODUTOS
                                   SET
                                        NOME = @Nome,
                                        PRECO = @Preco,
                                        QUANTIDADE = @Quantidade,
                                        DATACOMPRA = @DataCompra
                                   WHERE
                                        IDPRODUTO = @IdProduto
                                   """, produto);
            }
        }

        public void ExcluirProduto(Guid idProduto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                                   DELETE FROM PRODUTOS
                                   WHERE IDPRODUTO = @IdProduto
                                   """, new { @IdProduto = idProduto });
            }
        }

        public List<Produto> ObterTodosProdutos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Produto, Fornecedor, Categoria, Produto>("""
                                                 SELECT 
                                                        P.IDPRODUTO, P.NOME, P.PRECO, P.QUANTIDADE, P.DATACOMPRA,
                                                        F.IDFORNECEDOR, F.NOME, F.CNPJ,
                                                        C.IDCATEGORIA, C.DESCRICAO
                                                 FROM PRODUTOS P
                                                 INNER JOIN FORNECEDORES F ON P.IDFORNECEDOR = F.IDFORNECEDOR
                                                 INNER JOIN CATEGORIAS C ON P.IDCATEGORIA = C.IDCATEGORIA
                                                 ORDER BY P.NOME
                                                 """,
                    (produto, fornecedor, categoria) =>
                    {
                        produto.Fornecedor = fornecedor;
                        produto.Categoria = categoria;
                        return produto;
                    },
                    splitOn: "IDFORNECEDOR,IDCATEGORIA").ToList(); 
            }
        }


        #endregion
    }
}
