using ControleDeBar.Dominio.ModuloGarcom;
using ControleDeBar.Dominio.ModuloMesa;
using Microsoft.Data.SqlClient;

namespace ControleDeBar.Infraestrutura.SqlServer.ModuloMesa
{
    public class RepositorioMesaEmSql : IRepositorioMesa
    {
        private readonly string _sql =
            "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=ControleDeBarDb;Integrated Security=True";
        public void CadastrarRegistro(Mesa novoRegistro)
        {
            var sqlInserir =
            @"INSERT INTO [TBMESA]
                (
                    [ID],
                    [NUMERO],
                    [CAPACIDADE],
                    [ESTAOCUPADA]
                )
             VALUES
                (
                    @ID,
                    @NUMERO,
                    @CAPACIDADE,
                    @ESTAOCUPADA
                );";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlInserir, con);

            ConfigurarParametrosMesa(novoRegistro, sqlCommand);

            con.Open();

            sqlCommand.ExecuteNonQuery();

            con.Close();
        }

        public bool EditarRegistro(Guid idRegistro, Mesa registroEditado)
        {
            var sqlEditar =
            @"UPDATE [TBMESA]
            SET
                [NUMERO] = @NUMERO,
                [CAPACIDADE] = @CAPACIDADE
            WHERE
                [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlEditar, con);

            registroEditado.Id = idRegistro;

            ConfigurarParametrosMesa(registroEditado, sqlCommand);

            con.Open();

            var linhasAfetadas = sqlCommand.ExecuteNonQuery();

            con.Close();

            return linhasAfetadas > 0;
        }

        public bool ExcluirRegistro(Guid idRegistro)
        {
            var sqlExcluir =
            @"DELETE FROM [TBMESA]
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

        public Mesa SelecionarRegistroPorId(Guid idRegistro)
        {
            var sqlSelecionarPorId =
            @"SELECT
                [ID],
                [NUMERO],
                [CAPACIDADE],
                [ESTAOCUPADA]
            FROM
                [TBMESA]
            WHERE
                [ID] = @ID";

            SqlConnection con = new SqlConnection(_sql);

            SqlCommand sqlCommand = new SqlCommand(sqlSelecionarPorId, con);

            sqlCommand.Parameters.AddWithValue("ID", idRegistro);

            con.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            Mesa? mesa = null;

            if (reader.Read())
                mesa = ConverterParaMesa(reader);

            con.Close();

            return mesa;
        }

        public List<Mesa> SelecionarRegistros()
        {
            var sqlSelecionarTodos =
            @"SELECT
                [ID],
                [NUMERO],
                [CAPACIDADE],
                [ESTAOCUPADA]
            FROM
                [TBMESA]";

            SqlConnection con = new SqlConnection(_sql);

            con.Open();

            SqlCommand sqlCommand = new SqlCommand(sqlSelecionarTodos, con);

            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            var mesas = new List<Mesa>();

            while (dataReader.Read())
            {
                var mesa = ConverterParaMesa(dataReader);
                mesas.Add(mesa);
            }

            con.Close();

            return mesas;
        }

        private Mesa ConverterParaMesa(SqlDataReader reader)
        {
            var mesa = new Mesa(
                Convert.ToInt32(reader["NUMERO"]),
                Convert.ToInt32(reader["CAPACIDADE"])
                );

            mesa.Id = Guid.Parse(reader["ID"].ToString());

            return mesa;
        }
        private void ConfigurarParametrosMesa(Mesa mesa, SqlCommand sqlCommand)
        {
            sqlCommand.Parameters.AddWithValue("ID", mesa.Id);
            sqlCommand.Parameters.AddWithValue("NUMERO", mesa.Numero);
            sqlCommand.Parameters.AddWithValue("CAPACIDADE", mesa.Capacidade);
            sqlCommand.Parameters.AddWithValue("ESTAOCUPADA", mesa.EstaOcupada);
        }
    }
}
