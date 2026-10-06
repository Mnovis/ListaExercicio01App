using Dapper;
using ListaExercicio01App.Entities;
using Microsoft.Data.SqlClient;

namespace ListaExercicio01App.Repositories
{
    public class FornecedorRepository
    {
            #region Atributos Privados

            private readonly string _connectionString = "Server=localhost,1435; Database=master; User Id=sa; Password=Coti@2026; TrustServerCertificate=True";

            #endregion

            #region Métodos

            public void InserirFornecedor(Fornecedor fornecedor)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute("""
                                  INSERT INTO FORNECEDORES(IDFORNECEDOR, NOME, CNPJ)
                                    VALUES(@IdFornecedor, @Nome, @Cnpj)
                                  """, fornecedor);
                }
            }

            public void AtualizarFornecedor(Fornecedor fornecedor)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute("""
                                       UPDATE FORNECEDORES
                                       SET
                                            NOME = @Nome,
                                            CNPJ = @Cnpj
                                       WHERE
                                            IDFORNECEDOR = @IdFornecedor
                                       """, fornecedor);
                }
            }

            public void ExcluirFornecedor(Guid id)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute("""
                                       DELETE FROM FORNECEDORES
                                       WHERE IDFORNECEDOR = @IdFornecedor
                                       """, new { @IdFornecedor = id });
                }
            }

            public List<Fornecedor> ObterTodosFornecedores()
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    return connection.Query<Fornecedor>("""
                                                 SELECT IDFORNECEDOR, NOME, CNPJ
                                                 FROM FORNECEDORES
                                                 ORDER BY NOME
                                                 """).ToList();
                }
            }

            public bool VerificarCnpj(string cnpj, Guid id = default)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var qtd = connection.QuerySingle<int>("""
                                                          SELECT COUNT(*)
                                                          FROM FORNECEDORES
                                                          WHERE CNPJ = @Cnpj
                                                          AND IDFORNECEDOR <> @IdFornecedor
                                                          """, new
                    {
                        @Cnpj = cnpj,
                        @IdFornecedor = id
                    });

                    return qtd > 0;
                }
            }

        #endregion


    }
    }