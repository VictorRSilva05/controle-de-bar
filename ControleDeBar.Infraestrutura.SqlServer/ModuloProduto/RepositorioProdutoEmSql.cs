using ControleDeBar.Dominio.ModuloProduto;
using Microsoft.Data.SqlClient;

namespace ControleDeBar.Infraestrutura.SqlServer.ModuloProduto
{
    public class RepositorioProdutoEmSql : IRepositorioProduto
    {
        private readonly string _sql =
          "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=ControleDeBarDb;Integrated Security=True";
        public void CadastrarRegistro(Produto novoRegistro)
        {
            var sqlInserir =
                @"INSERT INTO [TBPRODUTO]
               (
                [ID],
                [NOME],
                [VALOR]
               )
                VALUES
               (
                @ID,
                @NOME,
                @VALOR
               );";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlInserir, con);

            ConfigurarParametrosProduto(novoRegistro, sqlCommand);

            con.Open();

            sqlCommand.ExecuteNonQuery();

            con.Close();
        }

        public bool EditarRegistro(Guid idRegistro, Produto registroEditado)
        {
            var sqlEditar =
            @"UPDATE [TBPRODUTO]
            SET
                [NOME] = @NOME,
                [VALOR] = @VALOR
            WHERE
                [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlEditar, con);

            registroEditado.Id = idRegistro;

            ConfigurarParametrosProduto(registroEditado, sqlCommand);

            con.Open();

            var linhasAfetadas = sqlCommand.ExecuteNonQuery();

            con.Close();

            return linhasAfetadas > 0;
        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            var sqlExcluir =
            @"DELETE FROM [TBPRODUTO]
                WHERE
            [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlExcluir, con);

            sqlCommand.Parameters.AddWithValue("ID", idRegistro);

            con.Open();

            var linhasAfetadas = sqlCommand.ExecuteNonQuery();

            con.Close();

            return linhasAfetadas > 0;
        }

        public Produto SelecionarRegistroPorId(Guid idRegistro)
        {
            var sqlSelecionarPorId =
            @"SELECT
                [ID],
                [NOME],
                [VALOR]
            FROM
                [TBPRODUTO]
            WHERE
                [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlSelecionarPorId, con);

            sqlCommand.Parameters.AddWithValue("ID", idRegistro);

            con.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            Produto? produto = null;

            if (reader.Read())
                produto = ConverterParaProduto(reader);

            con.Close();

            return produto;
        }

        public List<Produto> SelecionarRegistros()
        {
            var sqlSelecionarTodos =
            @"SELECT
                [ID],
                [NOME],
                [VALOR]
            FROM
                [TBPRODUTO]";

            SqlConnection con = new SqlConnection(_sql);

            con.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlSelecionarTodos, con);

            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            var produtos = new List<Produto>();

            while (dataReader.Read())
            {
                var produto = ConverterParaProduto(dataReader);

                produtos.Add(produto);
            }

            con.Close();

            return produtos;
        }

        private Produto ConverterParaProduto(SqlDataReader reader)
        {
            var produto = new Produto(
                Convert.ToString(reader["NOME"]),
                Convert.ToDecimal(reader["VALOR"])
                );

            produto.Id = Guid.Parse(reader["ID"].ToString());

            return produto;
        }

        private void ConfigurarParametrosProduto(Produto produto, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", produto.Id);
            sqlCommand.Parameters.AddWithValue("NOME", produto.Nome);
            sqlCommand.Parameters.AddWithValue("VALOR", produto.Valor);
        }
    }
}
