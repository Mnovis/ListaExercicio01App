using Dapper;
using ListaExercicio01App.Entities;
using Microsoft.Data.SqlClient;

namespace ListaExercicio01App.Repositories
{
    public class CategoriaRepository
    {
        #region Atributos Privados

        private readonly string _connectionString = "Server=localhost,1435; Database=master; User Id=sa; Password=Coti@2026; TrustServerCertificate=True";

        #endregion

        #region Métodos

        public void InserirCategoria(Categoria categoria)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                                  INSERT INTO CATEGORIAS(IDCATEGORIA, DESCRICAO)
                                    VALUES(@IdCategoria, @Descricao)
                                  """, categoria);
            }
        }

        public void AtualizarCategoria(Categoria categoria)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                                   UPDATE CATEGORIAS
                                   SET
                                        DESCRICAO = @Descricao
                                   WHERE
                                        IDCATEGORIA = @IdCategoria
                                   """, categoria);
            }
        }

        public void ExcluirCategoria(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("""
                                   DELETE FROM CATEGORIAS
                                   WHERE IDCATEGORIA = @IdCategoria
                                   """, new { @IdCategoria = id});
            }
        }

        public List<Categoria> ObterTodasCategorias()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Categoria>("""
                                                SELECT IDCATEGORIA, DESCRICAO
                                                FROM CATEGORIAS
                                                ORDER BY DESCRICAO
                                                """).ToList();
            }
        }

        #endregion


    }
}
