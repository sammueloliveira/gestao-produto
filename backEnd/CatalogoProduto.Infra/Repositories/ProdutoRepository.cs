using CatalogoProduto.Domain.Entities;
using CatalogoProduto.Infra.Data;
using Npgsql;

namespace CatalogoProduto.Infra.Repositories
{
    public class ProdutoRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ProdutoRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Produto>> GetAllAsync()
        {
            var produtos = new List<Produto>();
            var query = "SELECT * FROM produtos";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var departamentoCodigo = reader["departamentos"]?.ToString();
                            Departamento departamento = null;

                            if (!string.IsNullOrEmpty(departamentoCodigo))
                            {
                                departamento = new Departamento
                                {
                                    Codigo = departamentoCodigo,
                                    Descricao = MapearDescricaoDepartamento(departamentoCodigo)
                                };
                            }

                            produtos.Add(new Produto
                            {
                                Id = Guid.Parse(reader["id"].ToString()),
                                Codigo = reader["codigo"].ToString(),
                                Descricao = reader["descricao"].ToString(),
                                Departamentos = departamento,  
                                Preco = Convert.ToDecimal(reader["preco"]),
                                Status = Convert.ToBoolean(reader["status"])
                            });
                        }
                    }
                }
            }

            return produtos;
        }


        public async Task<Produto> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM produtos WHERE id = @Id";
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var departamentoCodigo = reader["departamentos"]?.ToString();
                            Departamento departamento = null;

                            if (!string.IsNullOrEmpty(departamentoCodigo))
                            {
                                departamento = new Departamento
                                {
                                    Codigo = departamentoCodigo,
                                    Descricao = MapearDescricaoDepartamento(departamentoCodigo)
                                };
                            }

                            return new Produto
                            {
                                Id = Guid.Parse(reader["id"].ToString()),
                                Codigo = reader["codigo"].ToString(),
                                Descricao = reader["descricao"].ToString(),
                                Departamentos = departamento, 
                                Preco = Convert.ToDecimal(reader["preco"]),
                                Status = Convert.ToBoolean(reader["status"])
                            };
                        }
                    }
                }
            }
            return null;
        }


        public async Task AddAsync(Produto produto)
        {
            var query = @"INSERT INTO produtos (id, codigo, descricao, departamentos, preco, status) 
                  VALUES (@Id, @Codigo, @Descricao, @Departamentos, @Preco, @Status)";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", produto.Id);
                    command.Parameters.AddWithValue("@Codigo", produto.Codigo);
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    command.Parameters.AddWithValue("@Departamentos", produto.Departamentos?.Codigo);  
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@Status", produto.Status);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task UpdateAsync(Produto produto)
        {
            var query = @"UPDATE produtos 
                  SET codigo = @Codigo, descricao = @Descricao, departamentos = @Departamentos, 
                      preco = @Preco, status = @Status 
                  WHERE id = @Id";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", produto.Id);
                    command.Parameters.AddWithValue("@Codigo", produto.Codigo);
                    command.Parameters.AddWithValue("@Descricao", produto.Descricao);
                    command.Parameters.AddWithValue("@Departamentos", produto.Departamentos?.Codigo);  
                    command.Parameters.AddWithValue("@Preco", produto.Preco);
                    command.Parameters.AddWithValue("@Status", produto.Status);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM produtos WHERE id = @Id";

            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private string MapearDescricaoDepartamento(string codigo)
        {
           
            var departamentos = new Dictionary<string, string>
            {
                { "010", "BEBIDAS" },
                { "020", "CONGELADOS" },
                { "030", "LATICINIOS" },
                { "040", "VEGETAIS" }
            };

            return departamentos.ContainsKey(codigo) ? departamentos[codigo] : "Desconhecido";
        }

        public bool ValidarDepartamento(string codigo, string descricao)
        {
           
            var departamentosPermitidos = new Dictionary<string, string>
            {
                 { "010", "BEBIDAS" },
                 { "020", "CONGELADOS" },
                 { "030", "LATICINIOS" },
                 { "040", "VEGETAIS" }
            };

           
            return departamentosPermitidos.ContainsKey(codigo) && departamentosPermitidos[codigo] == descricao;
        }

    }
}
